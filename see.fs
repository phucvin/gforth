\ SEE.FS       highend SEE for ANSforth                16may93jaw

\ Copyright (C) 1995 Free Software Foundation, Inc.

\ This file is part of Gforth.

\ Gforth is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public License
\ as published by the Free Software Foundation; either version 2
\ of the License, or (at your option) any later version.

\ This program is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program; if not, write to the Free Software
\ Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.


\ May be cross-compiled

\ I'm sorry. This is really not "forthy" enough.

\ Ideas:        Level should be a stack

require look.fs
require termsize.fs
require wordinfo.fs
[IFUNDEF] .name : .name name>string type space ; [THEN]

decimal

\ Screen format words                                   16may93jaw

VARIABLE C-Output   1 C-Output  !
VARIABLE C-Formated 1 C-Formated !
VARIABLE C-Highlight 0 C-Highlight !
VARIABLE C-Clearline 0 C-Clearline !

VARIABLE XPos
VARIABLE YPos
VARIABLE Level

: Format        C-Formated @ C-Output @ and
                IF dup spaces XPos +! ELSE drop THEN ;

: level+        7 Level +!
                Level @ XPos @ -
                dup 0> IF Format ELSE drop THEN ;

: level-        -7 Level +! ;

VARIABLE nlflag
VARIABLE uppercase	\ structure words are in uppercase

DEFER nlcount ' noop IS nlcount

: nl            nlflag on ;
: (nl)          nlcount
                XPos @ Level @ = IF EXIT THEN \ ?Exit
                C-Formated @ IF
                C-Output @
                IF C-Clearline @ IF cols XPos @ - spaces
                                 ELSE cr THEN
                1 YPos +! 0 XPos !
                Level @ spaces
                THEN Level @ XPos ! THEN ;

: warp?         ( len -- len )
                nlflag @ IF (nl) nlflag off THEN
                XPos @ over + cols u>= IF (nl) THEN ;

: c-to-upper
  dup [char] a >= over [char] z <= and if  bl -  then ;

: ctype         ( adr len -- )
                warp? dup XPos +! C-Output @ 
		IF uppercase @ IF bounds ?DO i c@ c-to-upper emit LOOP
				  uppercase off ELSE type THEN
		ELSE 2drop THEN ;

: cemit         1 warp?
                over bl = Level @ XPos @ = and
                IF 2drop ELSE XPos +! C-Output @ IF emit ELSE drop THEN
                THEN ;

DEFER .string

[IFDEF] Green
VARIABLE Colors Colors on

: (.string)     ( c-addr u n -- )
                over warp? drop
                Colors @
                IF C-Highlight @ ?dup
                   IF   CT@ swap CT@ or
                   ELSE CT@
                   THEN
                attr! ELSE drop THEN
                ctype  ct @ attr! ;
[ELSE]
: (.string)     ( c-addr u n -- )
                drop ctype ;
[THEN]

' (.string) IS .string


: .struc        
	uppercase on Str# .string ;

\ CODES (Branchtypes)                                    15may93jaw

21 CONSTANT RepeatCode
22 CONSTANT AgainCode
23 CONSTANT UntilCode
\ 09 CONSTANT WhileCode
10 CONSTANT ElseCode
11 CONSTANT AheadCode
13 CONSTANT WhileCode2
14 CONSTANT Disable
15 CONSTANT LeaveCode


\ FORMAT WORDS                                          13jun93jaw

VARIABLE C-Stop
VARIABLE Branches

VARIABLE BranchPointer	\ point to the end of branch table
VARIABLE SearchPointer

\ The branchtable consists of three entrys:
\ address of branch , branch destination , branch type

CREATE BranchTable 500 allot
here 3 cells -
ACONSTANT MaxTable

: FirstBranch BranchTable cell+ SearchPointer ! ;

: (BranchAddr?) ( a-addr1 -- a-addr2 true | false )
\ searches a branch with destination a-addr1
\ a-addr1: branch destination
\ a-addr2: pointer in branch table
        SearchPointer @
        BEGIN   dup BranchPointer @ u<
        WHILE
                dup @ 2 pick <>
        WHILE   3 cells +
        REPEAT
        nip dup  3 cells + SearchPointer ! true
        ELSE
        2drop false
        THEN ;

: BranchAddr?
        FirstBranch (BranchAddr?) ;

' (BranchAddr?) ALIAS MoreBranchAddr?

: CheckEnd ( a-addr -- true | false )
        BranchTable cell+
        BEGIN   dup BranchPointer @ u<
        WHILE
                dup @ 2 pick u<=
        WHILE   3 cells +
        REPEAT
        2drop false
        ELSE
        2drop true
        THEN ;

: MyBranch      ( a-addr -- a-addr a-addr2 )
\ finds branch table entry for branch at a-addr
                dup @ over +
                BranchAddr?
                BEGIN
                WHILE 1 cells - @
                      over <>
                WHILE dup @ over +
                      MoreBranchAddr?
                REPEAT
                SearchPointer @ 3 cells -
                ELSE    true ABORT" SEE: Table failure"
                THEN ;

\
\                 addrw               addrt
\       BEGIN ... WHILE ... AGAIN ... THEN
\         ^         !        !          ^
\         ----------+--------+          !
\                   !                   !
\                   +-------------------+
\
\

: CheckWhile ( a-addrw a-addrt -- true | false )
        BranchTable
        BEGIN   dup BranchPointer @ u<
        WHILE   dup @ 3 pick u>
                over @ 3 pick u< and
                IF dup cell+ @ 3 pick u<
                        IF 2drop drop true EXIT THEN
                THEN
                3 cells +
        REPEAT
        2drop drop false ;

: ,Branch ( a-addr -- )
        BranchPointer @ dup MaxTable u> ABORT" SEE: Table overflow"
        !
        1 cells BranchPointer +! ;

: Type!   ( u -- )
        BranchPointer @ 1 cells - ! ;

: Branch! ( a-addr rel -- a-addr )
        over + over ,Branch ,Branch 0 ,Branch ;

\ DEFER CheckUntil
VARIABLE NoOutput
VARIABLE C-Pass

0 CONSTANT ScanMode
1 CONSTANT DisplayMode
2 CONSTANT DebugMode

: Scan? ( -- flag ) C-Pass @ 0= ;
: Display? ( -- flag ) C-Pass @ 1 = ;
: Debug? ( -- flag ) C-Pass @ 2 = ;

: back? ( n -- flag ) 0< ;
: ahead? ( n -- flag ) 0> ;

: c-(compile)
    Display?
    IF
	s" POSTPONE " Com# .string
	dup @ look 0= ABORT" SEE: No valid XT"
	name>string 0 .string bl cemit
    THEN
    cell+ ;

: c-lit
    Display? IF
	dup @ dup abs 0 <# #S rot sign #> 0 .string bl cemit
    THEN
    cell+ ;

: .name-without ( addr -- addr )
\ prints a name without () e.g. (+LOOP) or (s")
  dup 1 cells - @ look 
  IF   name>string over c@ '( = IF 1 /string THEN
       2dup + 1- c@ ') = IF 1- THEN .struc ELSE drop 
  THEN ;

: c-c"
	Display? IF nl .name-without THEN
        count 2dup + aligned -rot
        Display?
        IF      bl cemit 0 .string
                [char] " cemit bl cemit
        ELSE    2drop
        THEN ;


: Forward? ( a-addr true | false -- a-addr true | false )
\ a-addr1 is pointer into branch table
\ returns true when jump is a forward jump
        IF      dup dup @ swap 1 cells - @ -
                Ahead? IF true ELSE drop false THEN
                \ only if forward jump
        ELSE    false THEN ;

: RepeatCheck ( a-addr1 a-addr2 true | false -- false )
        IF  BEGIN  2dup
                   1 cells - @ swap dup @ +
                   u<=
            WHILE  drop dup cell+
                   MoreBranchAddr? 0=
            UNTIL  false
            ELSE   true
            THEN
        ELSE false
        THEN ;

: c-branch
        Scan?
        IF      dup @ Branch!
                dup @ back?
                IF                      \ might be: AGAIN, REPEAT
                        dup cell+ BranchAddr? Forward?
                        RepeatCheck
                        IF      RepeatCode Type!
                                cell+ Disable swap !
                        ELSE    AgainCode Type!
                        THEN
                ELSE    dup cell+ BranchAddr? Forward?
                        IF      ElseCode Type! drop
                        ELSE    AheadCode Type!
                        THEN
                THEN
        THEN
        Display?
        IF
                dup @ back?
                IF                      \ might be: AGAIN, REPEAT
                        level- nl
                        dup cell+ BranchAddr? Forward?
                        RepeatCheck
                        IF      drop S" REPEAT " .struc nl
                        ELSE    S" AGAIN " .struc nl
                        THEN
                ELSE    MyBranch cell+ @ LeaveCode =
			IF 	S" LEAVE " .struc
			ELSE
				dup cell+ BranchAddr? Forward?
       	                 	IF      dup cell+ @ WhileCode2 =
       	                         	IF nl S" ELSE" .struc level+
                                	ELSE level- nl S" ELSE" .struc level+ THEN
                                	cell+ Disable swap !
                        	ELSE    S" AHEAD" .struc level+
                        	THEN
			THEN
                THEN
        THEN
        Debug?
        IF      dup @ +
        ELSE    cell+
        THEN ;

: DebugBranch
        Debug?
        IF      dup @ over + swap THEN ; \ return 2 different addresses

: c-?branch
        Scan?
        IF      dup @ Branch!
                dup @ Back?
                IF      UntilCode Type! THEN
        THEN
        Display?
        IF      dup @ Back?
                IF      level- nl S" UNTIL " .struc nl
                ELSE    dup    dup @ over +
                        CheckWhile
                        IF      MyBranch
                                cell+ dup @ 0=
                                         IF WhileCode2 swap !
                                         ELSE drop THEN
                                level- nl
                                S" WHILE " .struc
                                level+
                        ELSE    MyBranch cell+ @ LeaveCode =
				IF   s" 0= ?LEAVE " .struc
				ELSE nl S" IF " .struc level+
				THEN
                        THEN
                THEN
        THEN
        DebugBranch
        cell+ ;

: c-for
        Display? IF nl S" FOR" .struc level+ THEN ;

: c-loop
        Display? IF level- nl .name-without bl cemit nl THEN
        DebugBranch cell+ 
	Scan? 
	IF 	dup BranchAddr? 
		BEGIN   WHILE cell+ LeaveCode swap !
			dup MoreBranchAddr?
		REPEAT
	THEN
	cell+ ;

: c-do
        Display? IF nl .name-without level+ THEN ;

: c-?do
        Display? IF nl S" ?DO" .struc level+ THEN
        DebugBranch cell+ ;

: c-exit  dup 1 cells -
        CheckEnd
        IF      Display? IF nlflag off S" ;" Com# .string THEN
                C-Stop on
        ELSE    Display? IF S" EXIT " .struc THEN
        THEN
        Debug? IF drop THEN ;

: c-does>               \ end of create part
        Display? IF S" DOES> " Com# .string THEN
        Cell+ cell+ ;

: c-abort"
        count 2dup + aligned -rot
        Display?
        IF      S" ABORT" .struc
                [char] " cemit bl cemit 0 .string
                [char] " cemit bl cemit
        ELSE    2drop
        THEN ;


CREATE C-Table
	        ' lit A,            ' c-lit A,
		' (s") A,	    ' c-c" A,
       		 ' (.") A,	    ' c-c" A,
        	' "lit A,           ' c-c" A,
[IFDEF] (c")	' (c") A,	    ' c-c" A, [THEN]
        	' (do) A,           ' c-do A,
[IFDEF] (+do)	' (+do) A,	    ' c-do A, [THEN]
[IFDEF] (u+do)	' (u+do) A,	    ' c-do A, [THEN]
[IFDEF] (-do)	' (-do) A,	    ' c-do A, [THEN]
[IFDEF] (u-do)	' (u-do) A,	    ' c-do A, [THEN]
        	' (?do) A,          ' c-?do A,
        	' (for) A,          ' c-for A,
        	' ?branch A,        ' c-?branch A,
        	' branch A,         ' c-branch A,
        	' (loop) A,         ' c-loop A,
        	' (+loop) A,        ' c-loop A,
[IFDEF] (s+loop) ' (s+loop) A,       ' c-loop A, [THEN]
[IFDEF] (-loop) ' (-loop) A,        ' c-loop A, [THEN]
        	' (next) A,         ' c-loop A,
        	' ;s A,             ' c-exit A,
        	' (does>) A,        ' c-does> A,
        	' (abort") A,       ' c-abort" A,
        	' (compile) A,      ' c-(compile) A,
        	0 ,		here 0 ,

avariable c-extender
c-extender !

\ DOTABLE                                               15may93jaw

: DoTable ( cfa -- flag )
        C-Table
        BEGIN   dup @ dup 0= 
		IF drop cell+ @ dup 
		  IF ( next table!) dup @ ELSE 
			( end!) 2drop false EXIT THEN 
		THEN
		\ jump over to extender, if any 26jan97jaw
       		2 pick <>
        WHILE   2 cells +
        REPEAT
        nip cell+ perform
        true
	;

: BranchTo? ( a-addr -- a-addr )
        Display?  IF    dup BranchAddr?
                        IF
				BEGIN cell+ @ dup 20 u>
                                IF drop nl S" BEGIN " .struc level+
                                ELSE
                                  dup Disable <> over LeaveCode <> and
                                  IF   WhileCode2 =
                                       IF nl S" THEN " .struc nl ELSE
                                       level- nl S" THEN " .struc nl THEN
                                  ELSE drop THEN
                                THEN
                                  dup MoreBranchAddr? 0=
                           UNTIL
                        THEN
                  THEN ;

: analyse ( a-addr1 -- a-addr2 )
        Branches @ IF BranchTo? THEN
        dup cell+ swap @
        dup >r DoTable r> swap IF drop EXIT THEN
        Display?
        IF look 0= IF  drop dup 1 cells - @ .  \ ABORT" SEE: Bua!"
	ELSE
	    dup cell+ count dup immediate-mask and
	    IF  bl cemit  ." POSTPONE " THEN
	    31 and rot wordinfo .string  THEN  bl cemit
        ELSE drop
        THEN ;

: c-init
        0 YPos ! 0 XPos !
        0 Level ! nlflag off
        BranchTable BranchPointer !
        c-stop off
        Branches on ;

: makepass ( a-addr -- )
    c-stop off
    BEGIN
	analyse
	c-stop @
    UNTIL drop ;

Defer xt-see-xt ( xt -- )
\ this one is just a forward declaration for indirect recursion

: .defname ( xt c-addr u -- )
    rot look
    if ( c-addr u nfa )
	-rot type space .name
    else
	drop ." noname " type
    then
    space ;

Defer discode ( addr -- )
\  hook for the disassembler: disassemble code at addr (as far as the
\  disassembler thinks is sensible)
:noname ( addr -- )
    drop ." ..." ;
IS discode

: seecode ( xt -- )
    dup s" Code" .defname
    >body discode
    ."  end-code" cr ;
: seevar ( xt -- )
    s" Variable" .defname cr ;
: seeuser ( xt -- )
    s" User" .defname cr ;
: seecon ( xt -- )
    dup >body ?
    s" Constant" .defname cr ;
: seevalue ( xt -- )
    dup >body ?
    s" Value" .defname cr ;
: seedefer ( xt -- )
    dup >body @ xt-see-xt cr
    dup s" Defer" .defname cr
    >name dup ??? = if
	drop ." lastxt >body !"
    else
	." IS " .name cr
    then ;
: see-threaded ( addr -- )
    C-Pass @ DebugMode = IF
	ScanMode c-pass !
	EXIT
    THEN
    ScanMode c-pass ! dup makepass
    DisplayMode c-pass ! makepass ;
: seedoes ( xt -- )
    dup s" create" .defname cr
    S" DOES> " Com# .string XPos @ Level !
    >does-code see-threaded ;
: seecol ( xt -- )
    dup s" :" .defname nl
    2 Level !
    >body see-threaded ;
: seefield ( xt -- )
    dup >body ." 0 " ? ." 0 0 "
    s" Field" .defname cr ;

: xt-see ( xt -- )
    cr c-init
    dup >does-code
    if
	seedoes EXIT
    then
    dup xtprim?
    if
	seecode EXIT
    then
    dup >code-address
    CASE
	docon: of seecon endof
	docol: of seecol endof
	dovar: of seevar endof
[ [IFDEF] douser: ]
	douser: of seeuser endof
[ [THEN] ]
[ [IFDEF] dodefer: ]
	dodefer: of seedefer endof
[ [THEN] ]
[ [IFDEF] dofield: ]
	dofield: of seefield endof
[ [THEN] ]
	over >body of seecode endof
	2drop abort" unknown word type"
    ENDCASE ;

: (xt-see-xt) ( xt -- )
    xt-see cr ." lastxt" ;
' (xt-see-xt) is xt-see-xt

: (.immediate) ( xt -- )
    ['] execute = if
	."  immediate"
    then ;

: name-see ( nfa -- )
    dup name>int >r
    dup name>comp 
    over r@ =
    if \ normal or immediate word
	swap xt-see (.immediate)
    else
	r@ ['] compile-only-error =
	if \ compile-only word
	    swap xt-see (.immediate) ."  compile-only"
	else \ interpret/compile word
	    r@ xt-see-xt cr
	    swap xt-see-xt cr
	    ." interpret/compile " over .name (.immediate)
	then
    then
    rdrop drop ;

: see ( "name" -- ) \ tools
    name find-name dup 0=
    IF
	drop -&13 bounce
    THEN
    name-see ;



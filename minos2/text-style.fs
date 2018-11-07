\ MINOS2 text style

\ Copyright (C) 2018 Free Software Foundation, Inc.

\ This file is part of Gforth.

\ Gforth is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public License
\ as published by the Free Software Foundation, either version 3
\ of the License, or (at your option) any later version.

\ This program is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program. If not, see http://www.gnu.org/licenses/.

$000000FF color: blackish
$0000BFFF color: dark-blue
$00000000 color: transparent
$FFFFFFFF color: whitish
blackish
0e FValue x-border
: cbl ( -- )
    current-baseline% to x-baseline ;
: \skip ( -- )
    x-baseline 140% f* to x-baseline ;
: >bl ( o -- o' )
    >o x-baseline fdup to baseline gap% f* to gap o o> cbl ;
: }}text ( addr u -- o )
    text new >o font@ text! x-color to text-color  x-border to border o o> ;
: }}i18n-text ( lsid -- o )
    text new >o
    font@ i18n-text! x-color to text-color  x-border to border o o> ;
Defer }}text' ' }}text IS }}text'
: }}smalltext ( addr u -- o )
    font-size >r \script }}text' r> to font-size ;
: }}emoji ( addr u -- o )
    text new >o font@ text! x-color to text-color  x-border to border o o> ;
: }}edit ( addr u -- o )
    edit new >o font@ edit! x-color to text-color  x-border to border o o> ;
: }}pw ( addr u -- o )
    pw-edit new >o font@ edit! x-color to text-color  x-border to border
    1 to pw-mode  o o> ;
: >bdr ( o -- o' )
    >o font-size# to border o o> ;
: /center ( o -- o' )
    >r {{ glue*l }}glue r> glue*l }}glue }}h box[] >bl ;
: /left ( o -- o' )
    >r {{ r> glue*l }}glue }}h box[] >bl ;
: \\ }}text' /left ;
: p\\  }}text' >r {{ r> glue*l }}glue }}p box[] >bl
    dpy-w @ s>f font-size# 140% f* f- 1e text-shrink% f2/ f- f/ dup .par-split
    unbox ;
: e\\ }}emoji >r }}text' >r {{ r> glue*l }}glue r> }}h box[] >bl ;
: /right ( o -- o' )
    >r {{ glue*l }}glue r> }}h box[] >bl ;
: /hflip ( o -- o )
    >o box-hflip# to box-flags o o> ;
: /vflip ( o -- o )
    >o box-vflip# to box-flags o o> ;
: /dflip ( o -- o )
    >o box-dflip# to box-flags o o> ;
: /flip ( o -- o )
    >o box-flip# to box-flags o o> ;
: /hphantom ( o -- o )
    >o box-hphantom# to box-flags o o> ;
: /vphantom ( o -- o )
    >o box-vphantom# to box-flags o o> ;
: /phantom ( o -- o )
    >o box-phantom# to box-flags o o> ;
: /flop ( o -- o )
    >o 0 to box-flags o o> ;
Variable image-tex[]
Variable image-file[]
: }}image-file ( xt addr u r -- o glue-o ) pixelsize# f*
    2 pick image-tex[] >stack
    file>fpath $make dup image-file[] >stack dup cell+ swap @
    2 pick execute
    load-texture glue new >o
    s>f fover f* vglue-c df!
    s>f       f* hglue-c df! o o> dup >r
    $ffffffff color, rot }}image r> ;
: reload-images ( -- )
    image-tex[] $[]# 0 DO
	I image-tex[] $[] perform
	I image-file[] $[]@ load-texture 2drop
    LOOP ;
[IFDEF] android also android [THEN]
:noname defers reload-textures
    level# @ 0>  rendering @ -2 <= and  IF  reload-images  THEN ;
is reload-textures
[IFDEF] android previous [THEN]
: }}image-tex ( xt glue -- o )
    $ffffffff color, rot }}image ;

glue new Constant glue*wh

: update-glue
    glue*wh >o 0g 0g dpy-w @ s>f font-size# 140% f* f- hglue-c glue!
    0glue dglue-c glue! 1glue vglue-c glue! o> ;

update-glue

: tab-glue: ( glue "name" -- )
    Value DOES> @ swap >o to aidglue >glue0 o o> ;

0 tab-glue: bx-tab
glue new Constant glue*em
glue*em >o 1glue font-size# 0e 0e glue+ hglue-c glue! 0glue dglue-c glue! 1glue vglue-c glue! o>

: b0 ( addr1 u1 -- o )
    dark-blue }}text' >r
    {{ glue*em }}glue r> }}h box[] bx-tab
    blackish ;
: b\\ ( addr1 u1 addr2 u2 -- o ) \ blue black newline
    blackish }}text' >r
    b0 >r
    {{ r> r> glue*ll }}glue }}h box[] >bl ;
: bbe\\ ( addr1 u1 addr2 u2 addr3 u3 -- o ) \ blue black emoji newline
    }}emoji >r
    blackish }}text' >r
    b0 >r
    {{ r> r> r> glue*em }}glue }}h box[] >bl ;
: bi\\ ( addr1 u1 addr2 u2 -- o ) \ blue black newline
    blackish \italic }}text' >r
    \regular b0 >r
    {{ r> r> glue*em }}glue }}h box[] >bl ;
: _underline_ ( o -- o )
    >o 1 +to us-mask o o> ;
: __underline__ ( o -- o )
    >o 2 +to us-mask o o> ;
: ___underline___ ( o -- o )
    >o 3 +to us-mask o o> ;
: -strikethrough- ( o -- o )
    >o 4 +to us-mask o o> ;
: bm\\ ( addr1 u1 addr2 u2 -- o ) \ blue black newline
    dark-blue \mono }}text' _underline_ >r
    b0 >r
    {{ r> r> glue*em }}glue }}h box[] >bl \sans ;
: \LaTeX ( -- )
    "L" }}text font-size >r \script
    "A" }}text >o font-size# fdup -23% f* to raise -30% f* to kerning o o>
    r> to font-size
    "T" }}text >o font-size# -10% f* to kerning o o>
    "E" }}text >o font-size# -23% f* fdup fnegate to raise to kerning o o>
    "X" }}text >o font-size# -10% f* to kerning o o> ;
: nt ( -- ) htab-glue new to bx-tab ; \ new tab
: vt{{ nt {{ ;
: }}vt \ vertical box with tab
    }}v box[] ;

\ high level style

: /title ( addr u -- )
    \huge cbl \sans \latin \bold dark-blue }}text' /center blackish
    \normal \regular x-baseline 80% f* to x-baseline ;
: /subtitle ( addr u -- ) \small dark-blue }}text' /center blackish \normal ;
: /author ( addr u -- ) \normal \large \bold dark-blue }}text' /center blackish
    \normal \regular \skip ;
: /location ( addr u -- ) \normal  dark-blue }}text' /center blackish \normal ;
: /subsection ( addr u -- ) \normal \bold dark-blue \\ blackish \normal \regular ;

\ mode

: !i18n  ['] }}i18n-text IS }}text' ;
: !lit   ['] }}text IS }}text' ;

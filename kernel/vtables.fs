\ vtables.fs does the intelligent compile, vtable handling

\ Copyright (C) 2012,2013,2014 Free Software Foundation, Inc.

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

: value, >body ['] lit@ peephole-compile, , ;
: constant, >body @ ['] lit peephole-compile, , ;
: 2constant, >body 2@ swap
    ['] lit peephole-compile, ,
    ['] lit peephole-compile, , ;
: :, >body ['] call peephole-compile, , ;
: variable, >body ['] lit peephole-compile, , ;
: user, >body @ ['] useraddr peephole-compile, , ;
: defer, >body ['] lit-perform peephole-compile, , ;
: field+, >body @ ['] lit+ peephole-compile, , ;
: abi-code, >body ['] abi-call peephole-compile, , ;
: ;abi-code, ['] ;abi-code-exec peephole-compile, , ;
: does, ['] does-exec peephole-compile, , ;
: umethod, >body cell+ 2@ ['] u#exec peephole-compile, , , ;
: uvar, >body cell+ 2@ ['] u#+ peephole-compile, , , ;
: :loc, >body ['] call-loc peephole-compile, , ;

: (uv!) ( xt addr -- ) 2@ next-task + @ cell- @ swap cells + ! ;
: umethod! ( xt xt-method -- )
    >body cell+ (uv!) ;
comp: drop >body cell+ postpone Aliteral postpone (uv!) ;
: umethod@ ( addr -- xt ) >body cell+ 2@ next-task + @ cell- @ swap cells + @ ;

AVariable vtable-list

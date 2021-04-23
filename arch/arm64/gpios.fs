\ gpios.fs	GPIO access
\
\ Authors: Bernd Paysan,
\ Copyright (C) 2021 Free Software Foundation, Inc.

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

require unix/libc.fs
require unix/mmap.fs

0 Value gpio-base

: reg: ( offset -- )
    Create sfloats ,
    DOES> @ gpio-base + ;

s" /sys/firmware/devicetree/base/model" slurp-file 2Constant model

model s" ODROID-N2" search nip nip [IF]
    model s" ODROID-N2Plus" search nip nip [IF]
	: odroid-n2+ ;
    [ELSE]
	: odroid-n2 ;
    [THEN]
    $FF634000 Constant GPIO-Base-map

    $116 reg: N2_GPIOX_FSEL_REG
    $117 reg: N2_GPIOX_OUTP_REG
    $118 reg: N2_GPIOX_INP_REG
    $13C reg: N2_GPIOX_PUPD_REG
    $14A reg: N2_GPIOX_PUEN_REG
    $1D2 reg: N2_GPIOX_DS_REG_2A
    $1D3 reg: N2_GPIOX_DS_REG_2B
    $1B3 reg: N2_GPIOX_MUX_3_REG
    $1B4 reg: N2_GPIOX_MUX_4_REG
    $1B5 reg: N2_GPIOX_MUX_5_REG
    
    $120 reg: N2_GPIOA_FSEL_REG
    $121 reg: N2_GPIOA_OUTP_REG
    $122 reg: N2_GPIOA_INP_REG
    $13F reg: N2_GPIOA_PUPD_REG
    $14D reg: N2_GPIOA_PUEN_REG
    $1D6 reg: N2_GPIOA_DS_REG_5A
    $1BD reg: N2_GPIOA_MUX_D_REG
    $1BE reg: N2_GPIOA_MUX_E_REG
[THEN]
model s" ODROID-C2" search nip nip [IF]
    : odroid-c2 ;
    $C8834000 Constant GPIO-Base-map

    $118 reg: C2_GPIOX_FSEL_REG
    $119 reg: C2_GPIOX_OUTP_REG
    $11A reg: C2_GPIOX_INP_REG
    $13E reg: C2_GPIOX_PUPD_REG
    $14C reg: C2_GPIOX_PUEN_REG
    
    $10F reg: C2_GPIOY_FSEL_REG
    $110 reg: C2_GPIOY_OUTP_REG
    $111 reg: C2_GPIOY_INP_REG
    $13B reg: C2_GPIOY_PUPD_REG
    $149 reg: C2_GPIOY_PUEN_REG
    
    $10C reg: C2_GPIODV_FSEL_REG
    $10D reg: C2_GPIODV_OUTP_REG
    $10E reg: C2_GPIODV_INP_REG
    $148 reg: C2_GPIODV_PUPD_REG
    $13A reg: C2_GPIODV_PUEN_REG
    
    $12C reg: C2_MUX_REG_0
    $12D reg: C2_MUX_REG_1
    $12E reg: C2_MUX_REG_2
    $12F reg: C2_MUX_REG_3
    $130 reg: C2_MUX_REG_4
    $131 reg: C2_MUX_REG_5
    $133 reg: C2_MUX_REG_7
    $134 reg: C2_MUX_REG_8
[THEN]

: map-gpio ( -- )
    s" /dev/gpiomem" r/w open-file throw dup >r fileno >r
    0 $1000 PROT_READ PROT_WRITE or MAP_SHARED r> GPIO-Base-map mmap
    r> close-file throw dup 0= ?ior to gpio-base ;

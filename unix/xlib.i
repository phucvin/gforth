// this file is in the public domain
%module x
%insert("include")
%{
#include <X11/X.h>
#include <X11/Xlib.h>
#include <X11/Xutil.h>
#include <X11/keysym.h>
#include <X11/XF86keysym.h>
%}

#define SWIG_FORTH_GFORTH_LIBRARY "X11"

#define _XFUNCPROTOBEGIN
#define _XFUNCPROTOEND
#define _Xconst const
#define _X_DEPRECATED
#define _X_SENTINEL(x)
%apply short { wchar_t };

// exec: sed -e 's/ \(ws\|s\) \([nu]\) / a \2 /g' -e 's/XStoreNamedColor a u a u n/XStoreNamedColor a u s u n/g' -e 's/XInternAtom a a n/XInternAtom a s n/g' -e 's/XListFonts a a n a/XListFonts a s n a/g' -e 's/XListFontsWithInfo a a n a a/XListFontsWithInfo a s n a a/g' -e 's/XGeometry a n s a/XGeometry a n s s/g' -e 's/XStoreNamedColor a u a u n/XStoreNamedColor a u s u n/g' -e 's/XOpenDisplay a/XOpenDisplay s/g' -e 's/\(c-function [^ ]*\)\(.*\)\( \.\.\. .*\)/\1\2\3\n\1_2\2 a a\3\n\1_3\2 a a a a\3/g'

#define XK_MISCELLANY

%include <X11/X.h>
%include <X11/Xlib.h>
%include <X11/Xutil.h>
%include <X11/keysymdef.h>
%include <X11/XF86keysym.h>

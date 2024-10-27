LABEL(add) /* add ( #i1 #i2 #i3 -- ) */
/*  */
NAME("add")
{
DEF_CA
long i1;
long i2;
long i3;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397760 ),i1);
vm_Cell2i(IMM_ARG(IP[1],305397761 ),i2);
vm_Cell2i(IMM_ARG(IP[2],305397762 ),i3);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" i2=", vm_out); printarg_i(i2);
fputs(" i3=", vm_out); printarg_i(i3);
}
#endif
INC_IP(3);
{
#line 42 "mini.vmg"
reg[i1] = reg[i2]+reg[i3];
#line 25 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(add)
NEXT_P2;
}

LABEL(sub) /* sub ( #i1 #i2 #i3 -- ) */
/*  */
NAME("sub")
{
DEF_CA
long i1;
long i2;
long i3;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397763 ),i1);
vm_Cell2i(IMM_ARG(IP[1],305397764 ),i2);
vm_Cell2i(IMM_ARG(IP[2],305397765 ),i3);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" i2=", vm_out); printarg_i(i2);
fputs(" i3=", vm_out); printarg_i(i3);
}
#endif
INC_IP(3);
{
#line 45 "mini.vmg"
reg[i1] = reg[i2]-reg[i3];
#line 61 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(sub)
NEXT_P2;
}

LABEL(lt) /* lt ( #i1 #i2 #i3 -- ) */
/*  */
NAME("lt")
{
DEF_CA
long i1;
long i2;
long i3;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397766 ),i1);
vm_Cell2i(IMM_ARG(IP[1],305397767 ),i2);
vm_Cell2i(IMM_ARG(IP[2],305397768 ),i3);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" i2=", vm_out); printarg_i(i2);
fputs(" i3=", vm_out); printarg_i(i3);
}
#endif
INC_IP(3);
{
#line 48 "mini.vmg"
reg[i1] = reg[i2]<reg[i3];
#line 97 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(lt)
NEXT_P2;
}

LABEL(load) /* load ( #i1 -- i ) */
/*  */
NAME("load")
{
DEF_CA
long i1;
long i;
NEXT_P0;
IF_spTOS(sp[0] = spTOS);
vm_Cell2i(IMM_ARG(IPTOS,305397769 ),i1);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
}
#endif
INC_IP(1);
sp += -1;
{
#line 51 "mini.vmg"
i = reg[i1];
#line 130 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputs(" i=", vm_out); printarg_i(i);
fputc('\n', vm_out);
}
#endif
NEXT_P1;
vm_i2Cell(i,spTOS);
LABEL2(load)
NEXT_P2;
}

LABEL(store) /* store ( #i1 #i -- ) */
/*  */
NAME("store")
{
DEF_CA
long i1;
long i;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397770 ),i1);
vm_Cell2i(IMM_ARG(IP[1],305397771 ),i);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" i=", vm_out); printarg_i(i);
}
#endif
INC_IP(2);
{
#line 54 "mini.vmg"
reg[i1] = i;
#line 165 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(store)
NEXT_P2;
}

LABEL(branch) /* branch ( #target -- ) */
/*  */
NAME("branch")
{
DEF_CA
Inst * target;
NEXT_P0;
vm_Cell2target(IMM_ARG(IPTOS,305397772 ),target);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" target=", vm_out); printarg_target(target);
}
#endif
INC_IP(1);
{
#line 57 "mini.vmg"
SET_IP(target);
#line 195 "mini-vm.i"
}
SUPER_END;

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(branch)
NEXT_P2;
}

LABEL(zbranch) /* zbranch ( #i1 #target -- ) */
/*  */
NAME("zbranch")
{
DEF_CA
long i1;
Inst * target;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397773 ),i1);
vm_Cell2target(IMM_ARG(IP[1],305397774 ),target);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" target=", vm_out); printarg_target(target);
}
#endif
INC_IP(2);
{
#line 60 "mini.vmg"
if (reg[i1] == 0) {
  SET_IP(target);
  SUPER_END;

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
NEXT_P2;

}
#line 241 "mini-vm.i"
}
SUPER_END;

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(zbranch)
NEXT_P2;
}

LABEL(call) /* call ( #target #iadjust -- targetret aoldfp ) */
/*  */
NAME("call")
{
DEF_CA
Inst * target;
long iadjust;
Inst * targetret;
char * aoldfp;
NEXT_P0;
IF_spTOS(sp[0] = spTOS);
vm_Cell2target(IMM_ARG(IPTOS,305397775 ),target);
vm_Cell2i(IMM_ARG(IP[1],305397776 ),iadjust);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" target=", vm_out); printarg_target(target);
fputs(" iadjust=", vm_out); printarg_i(iadjust);
}
#endif
INC_IP(2);
sp += -2;
{
#line 86 "mini.vmg"
/* IF_spTOS(sp[2] = spTOS);*/ /* unnecessary; vmgen inserts a flush anyway */
targetret = IP;
SET_IP(target);
aoldfp = fp;
sp = (Cell *)(((char *)sp)+iadjust);
fp = (char *)sp;
/* IF_spTOS(spTOS = sp[0]); */ /* dead, thus unnecessary; vmgen copies aoldfp there */
#line 285 "mini-vm.i"
}
SUPER_END;

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputs(" targetret=", vm_out); printarg_target(targetret);
fputs(" aoldfp=", vm_out); printarg_a(aoldfp);
fputc('\n', vm_out);
}
#endif
NEXT_P1;
vm_target2Cell(targetret,sp[1]);
vm_a2Cell(aoldfp,spTOS);
LABEL2(call)
NEXT_P2;
}

LABEL(return) /* return ( #iadjust target afp i1 -- i2 ) */
/*  */
NAME("return")
{
DEF_CA
long iadjust;
Inst * target;
char * afp;
long i1;
long i2;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397777 ),iadjust);
vm_Cell2target(sp[2],target);
vm_Cell2a(sp[1],afp);
vm_Cell2i(spTOS,i1);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" iadjust=", vm_out); printarg_i(iadjust);
fputs(" target=", vm_out); printarg_target(target);
fputs(" afp=", vm_out); printarg_a(afp);
fputs(" i1=", vm_out); printarg_i(i1);
}
#endif
INC_IP(1);
sp += 2;
{
#line 95 "mini.vmg"
/* IF_spTOS(sp[-2] = spTOS); */ /* unnecessary; that stack item is dead */
SET_IP(target);
sp = (Cell *)(((char *)sp)+iadjust);
fp = afp;
i2=i1;
/* IF_spTOS(spTOS = sp[0]); */ /* dead, thus unnecessary; vmgen copies i2 there */
#line 336 "mini-vm.i"
}
SUPER_END;

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputs(" i2=", vm_out); printarg_i(i2);
fputc('\n', vm_out);
}
#endif
NEXT_P1;
vm_i2Cell(i2,spTOS);
LABEL2(return)
NEXT_P2;
}

LABEL(loadlocal) /* loadlocal ( #i1 #ioffset -- ) */
/*  */
NAME("loadlocal")
{
DEF_CA
long i1;
long ioffset;
NEXT_P0;
vm_Cell2i(IMM_ARG(IPTOS,305397778 ),i1);
vm_Cell2i(IMM_ARG(IP[1],305397779 ),ioffset);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i1=", vm_out); printarg_i(i1);
fputs(" ioffset=", vm_out); printarg_i(ioffset);
}
#endif
INC_IP(2);
{
#line 106 "mini.vmg"
reg[i1] = ((Cell *)(fp+ioffset))->i;
#line 372 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
LABEL2(loadlocal)
NEXT_P2;
}

LABEL(end) /* end ( i -- ) */
/*  */
NAME("end")
{
DEF_CA
long i;
NEXT_P0;
vm_Cell2i(spTOS,i);
#ifdef VM_DEBUG
if (vm_debug) {
fputs(" i=", vm_out); printarg_i(i);
}
#endif
sp += 1;
{
#line 109 "mini.vmg"
/* SUPER_END would increment the next BB count (because IP points there);
   this would be a problem if there is no following BB.
   Instead, we do the following to add an end point for the current BB: */
#ifdef VM_PROFILING
block_insert(IP); /* we also do this at compile time, so this is unnecessary */
#endif
return i;
#line 408 "mini-vm.i"
}

#ifdef VM_DEBUG
if (vm_debug) {
fputs(" -- ", vm_out); fputc('\n', vm_out);
}
#endif
NEXT_P1;
IF_spTOS(spTOS = sp[0]);
LABEL2(end)
NEXT_P2;
}


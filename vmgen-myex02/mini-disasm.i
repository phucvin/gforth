if (VM_IS_INST(*ip, 0)) {
  fputs("add", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397760 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
long i2;
vm_Cell2i(IMM_ARG(IP[1],305397761 ),i2);
fputs(" i2=", vm_out); printarg_i(i2);
}
{
long i3;
vm_Cell2i(IMM_ARG(IP[2],305397762 ),i3);
fputs(" i3=", vm_out); printarg_i(i3);
}
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 1)) {
  fputs("sub", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397763 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
long i2;
vm_Cell2i(IMM_ARG(IP[1],305397764 ),i2);
fputs(" i2=", vm_out); printarg_i(i2);
}
{
long i3;
vm_Cell2i(IMM_ARG(IP[2],305397765 ),i3);
fputs(" i3=", vm_out); printarg_i(i3);
}
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 2)) {
  fputs("lt", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397766 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
long i2;
vm_Cell2i(IMM_ARG(IP[1],305397767 ),i2);
fputs(" i2=", vm_out); printarg_i(i2);
}
{
long i3;
vm_Cell2i(IMM_ARG(IP[2],305397768 ),i3);
fputs(" i3=", vm_out); printarg_i(i3);
}
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 3)) {
  fputs("load", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397769 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 4)) {
  fputs("store", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397770 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
long i;
vm_Cell2i(IMM_ARG(IP[1],305397771 ),i);
fputs(" i=", vm_out); printarg_i(i);
}
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 5)) {
  fputs("branch", vm_out);
{
Inst * target;
vm_Cell2target(IMM_ARG(IPTOS,305397772 ),target);
fputs(" target=", vm_out); printarg_target(target);
}
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 6)) {
  fputs("zbranch", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397773 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
Inst * target;
vm_Cell2target(IMM_ARG(IP[1],305397774 ),target);
fputs(" target=", vm_out); printarg_target(target);
}
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 7)) {
  fputs("call", vm_out);
{
Inst * target;
vm_Cell2target(IMM_ARG(IPTOS,305397775 ),target);
fputs(" target=", vm_out); printarg_target(target);
}
{
long iadjust;
vm_Cell2i(IMM_ARG(IP[1],305397776 ),iadjust);
fputs(" iadjust=", vm_out); printarg_i(iadjust);
}
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 8)) {
  fputs("return", vm_out);
{
long iadjust;
vm_Cell2i(IMM_ARG(IPTOS,305397777 ),iadjust);
fputs(" iadjust=", vm_out); printarg_i(iadjust);
}
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 9)) {
  fputs("loadlocal", vm_out);
{
long i1;
vm_Cell2i(IMM_ARG(IPTOS,305397778 ),i1);
fputs(" i1=", vm_out); printarg_i(i1);
}
{
long ioffset;
vm_Cell2i(IMM_ARG(IP[1],305397779 ),ioffset);
fputs(" ioffset=", vm_out); printarg_i(ioffset);
}
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 10)) {
  fputs("end", vm_out);
  ip += 1;
  goto _endif_;
}

void gen_add(Inst **ctp, long i1, long i2, long i3)
{
  gen_inst(ctp, vm_prim[0]);
  genarg_i(ctp, i1);
  genarg_i(ctp, i2);
  genarg_i(ctp, i3);
}
void gen_sub(Inst **ctp, long i1, long i2, long i3)
{
  gen_inst(ctp, vm_prim[1]);
  genarg_i(ctp, i1);
  genarg_i(ctp, i2);
  genarg_i(ctp, i3);
}
void gen_lt(Inst **ctp, long i1, long i2, long i3)
{
  gen_inst(ctp, vm_prim[2]);
  genarg_i(ctp, i1);
  genarg_i(ctp, i2);
  genarg_i(ctp, i3);
}
void gen_load(Inst **ctp, long i1)
{
  gen_inst(ctp, vm_prim[3]);
  genarg_i(ctp, i1);
}
void gen_store(Inst **ctp, long i1, long i)
{
  gen_inst(ctp, vm_prim[4]);
  genarg_i(ctp, i1);
  genarg_i(ctp, i);
}
void gen_branch(Inst **ctp, Inst * target)
{
  gen_inst(ctp, vm_prim[5]);
  genarg_target(ctp, target);
}
void gen_zbranch(Inst **ctp, long i1, Inst * target)
{
  gen_inst(ctp, vm_prim[6]);
  genarg_i(ctp, i1);
  genarg_target(ctp, target);
}
void gen_call(Inst **ctp, Inst * target, long iadjust)
{
  gen_inst(ctp, vm_prim[7]);
  genarg_target(ctp, target);
  genarg_i(ctp, iadjust);
}
void gen_return(Inst **ctp, long iadjust)
{
  gen_inst(ctp, vm_prim[8]);
  genarg_i(ctp, iadjust);
}
void gen_loadlocal(Inst **ctp, long i1, long ioffset)
{
  gen_inst(ctp, vm_prim[9]);
  genarg_i(ctp, i1);
  genarg_i(ctp, ioffset);
}
void gen_end(Inst **ctp)
{
  gen_inst(ctp, vm_prim[10]);
}

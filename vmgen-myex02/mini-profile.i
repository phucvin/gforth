if (VM_IS_INST(*ip, 0)) {
  add_inst(b, "add");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 1)) {
  add_inst(b, "sub");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 2)) {
  add_inst(b, "lt");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 3)) {
  add_inst(b, "load");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 4)) {
  add_inst(b, "store");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 5)) {
  add_inst(b, "branch");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 6)) {
  add_inst(b, "zbranch");
  ip += 3;
  return;
}
if (VM_IS_INST(*ip, 7)) {
  add_inst(b, "call");
  ip += 3;
  return;
}
if (VM_IS_INST(*ip, 8)) {
  add_inst(b, "return");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 9)) {
  add_inst(b, "loadlocal");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 10)) {
  add_inst(b, "end");
  ip += 1;
  return;
}

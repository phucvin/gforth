cd ..

source ./install-deps.sh 

gfort-fast fib.fs

39 fib .

bye

cd ../vmgen-ex

sudo apt install flex

make

time ./mini fib.mini

> 4.2s

cd ../vmgen-ex2

make

time ./mini fib.mini

> 3.4s

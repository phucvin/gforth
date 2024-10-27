cd ..

source ./install-deps.sh 

gforth

cd ../vmgen-ex

sudo apt install flex

make

time ./mini fib.mini

> 4.2s

..\..\deps\llvm\build\RelWithDebInfo\bin\clang RuntimeType.cpp --target=i686-pc-mingw32 -std=c++11 -emit-llvm -c -o RuntimeType.bc -O3 -I../../deps/llvm/build/include -I../../deps/llvm/include -I../../deps/mingw32/i686-w64-mingw32/include -I../../deps/mingw32/i686-w64-mingw32/include/c++ -I../../deps/mingw32/i686-w64-mingw32/include/c++/i686-w64-mingw32 -D__STDC_CONSTANT_MACROS -D__STDC_LIMIT_MACROS
..\..\deps\llvm\build\RelWithDebInfo\bin\clang Exception.cpp --target=i686-pc-mingw32 -std=c++11 -emit-llvm -c -o Exception.bc -O3 -I../../deps/llvm/build/include -I../../deps/llvm/include -I../../deps/mingw32/i686-w64-mingw32/include -I../../deps/mingw32/i686-w64-mingw32/include/c++ -I../../deps/mingw32/i686-w64-mingw32/include/c++/i686-w64-mingw32 -D__STDC_CONSTANT_MACROS -D__STDC_LIMIT_MACROS
..\..\deps\llvm\build\RelWithDebInfo\bin\clang PInvoke.cpp --target=i686-pc-mingw32 -std=c++11 -emit-llvm -c -o PInvoke.bc -O3 -I../../deps/llvm/build/include -I../../deps/llvm/include -I../../deps/mingw32/i686-w64-mingw32/include -I../../deps/mingw32/i686-w64-mingw32/include/c++ -I../../deps/mingw32/i686-w64-mingw32/include/c++/i686-w64-mingw32 -D__STDC_CONSTANT_MACROS -D__STDC_LIMIT_MACROS
..\..\deps\llvm\build\RelWithDebInfo\bin\llvm-link RuntimeType.bc Exception.bc PInvoke.bc -o Runtime.bc
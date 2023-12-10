cd test-data

mkdir subfolder1 -Force
echo "Hello, World!" > hello.txt
echo "Goodbye, World!" > goodbye.txt
echo "Hello, Subfolder1!" > subfolder1/hello.txt
echo "Goodbye, Subfolder1!" > subfolder1/goodbye.txt

mkdir subfolder1/subfolder1a -Force
echo "Hello, Subfolder1a!" > subfolder1/subfolder1a/hello.txt
echo "Goodbye, Subfolder1a!" > subfolder1/subfolder1a/goodbye.txt

mkdir subfolder2 -Force
echo "Hello, Subfolder2!" > subfolder2/hello.txt
echo "Goodbye, Subfolder2!" > subfolder2/goodbye.txt

mkdir subfolder3 -Force

cd ..


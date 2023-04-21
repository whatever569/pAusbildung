import cs50

while True:
    height = cs50.get_int("Height: ")
    if height > 0 and height <= 8:
        break

z = height - 1
p = 1
for i in range(height):
    print(" " * z, end = "")
    print("#" * p, end = "")
    print("  ", end = "")
    print("#" * p, end = "")
    z = z - 1
    p = p + 1
    print()

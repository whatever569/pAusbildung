from cs50 import get_int

input = get_int("Number: ")
# the original input
orinput = input

length = len(str(input))
# if this number is even add to evenTtl else to oddTtl, it is also used as a number counter
count = 0
evenTtl = 0
oddTtl = 0

valid = False

for i in range(length):

    count += 1
    number = input % 10

    if count % 2 == 0:
        number *= 2

        if number > 9:
            number -= 9

        evenTtl += number

    else:
        oddTtl += number

    input = input // 10

if (evenTtl + oddTtl) % 10 == 0:
    valid = True
else:
    print("INVALID")


# AMERX 15 STRT 34 OR 37
# MC 16 STRT 51, 52, 53, 54, 55
# VZA 13 OR 16 STRT 4

if valid == True:
    if (orinput >= 4 * (10 ** 12) and orinput < 5 * (10 ** 12)) or (orinput >= 4 * (10 ** 15) and orinput < 5 * (10 ** 15)):
        print("VISA")
    elif (orinput >= 34 * (10 ** 13) and orinput < 35 * (10 ** 13)) or (orinput >= 37 * (10 ** 13) and orinput < 38 * (10 ** 13)):
        print("AMEX")
    elif orinput >= 51 * (10 ** 14) and orinput < (56 * (10 ** 14)):
        print("MASTERCARD")
    else:
        print("INVALID")

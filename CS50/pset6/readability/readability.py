from cs50 import get_string

input = get_string("Text: ")

ltrs = 0
wrds = len(input.split())
sntns = 0
len = len(input)
strtWrd = False

for i in range(len):
    if input[i].isalpha():
        ltrs += 1
        strtWrd = True
    if input[i] == "!" or input[i] == "?" or input[i] == ".":
        sntns += 1

index = 0.0588 * (ltrs / wrds * 100) - 0.296 * (sntns / wrds * 100) - 15.8;

if index > 16:
    print("Grade 16+")
elif index < 1:
    print("Before Grade 1")
else:
    print(f"Grade {round(index)}")


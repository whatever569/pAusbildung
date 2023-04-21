import cs50
from sys import argv


if len(argv) != 2:
    print("Error 304: Usage -> program <House>")
    exit()

db = cs50.SQL("sqlite:///students.db")

house = argv[1]

rows = db.execute("SELECT first, middle, last, birth FROM students WHERE house = ? ORDER BY last, first", house)

for row in rows:
    if row['middle'] == None:
        print(f"{row['first']} {row['last']}, born {row['birth']}")
    else:
        print(f"{row['first']} {row['middle']} {row['last']}, born {row['birth']}")
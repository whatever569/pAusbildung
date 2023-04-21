from sys import argv
import csv
import cs50
open("students.db", "w").close()
db = cs50.SQL("sqlite:///students.db")
db.execute("CREATE TABLE students (first TEXT, middle TEXT, last TEXT, house TEXT, birth NUMERIC)")
if len(argv) != 2:
    print("Error208: Usage -> program <csv>")
    exit()

with open(argv[1], "r") as fcsv:

    reader = csv.DictReader(fcsv)
    for row in reader:
        currentName = row['name'].split(" ")
        if len(currentName) != 3:
            first = currentName[0].strip()
            last = currentName[1].strip()
            house = row['house'].strip()
            birth = row['birth']
            db.execute("INSERT INTO students (first, last, house, birth) VALUES (?, ?, ?, ?)", first, last, house, birth)
        else:
            first = currentName[0].strip()
            middle = currentName[1].strip()
            last = currentName[2].strip()
            house = row['house'].strip()
            birth = row['birth']
            db.execute("INSERT INTO students (first, middle, last, house, birth) VALUES (?, ?, ?, ?, ?)", first, middle, last, house, birth)
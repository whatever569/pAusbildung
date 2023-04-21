import csv
from sys import argv
from sys import exit


def main():
    dict = {}
    rows = []
    values = []
    biggestCount = 0 
    currentCount = 0
    matchCounter = 0 
    match = []
    #first arg is csv amd second arg is dna
    if len(argv) != 3:
        print("Usage: program <csv> <dna>")
        exit()

    fdna = open(argv[2], 'r')
    fcsv = open(argv[1], 'r')

    text = fdna.read()
    reader = csv.reader(fcsv)
    for row in reader:
        rows.append(row)

    length_of_row = len(rows[0])
    lengthRows = len(rows)
    for i in range(1, length_of_row):
        scanKey = rows[0][i]
        scLength = len(scanKey)
        if not scanKey in text: 
            dict[scanKey] = 0
        else:
            currentText = text
            while scanKey in currentText:
                if scanKey in currentText[:scLength]:
                    currentCount += 1
                    currentText = currentText[scLength:len(currentText)]
                    
                    if currentCount > biggestCount:
                        biggestCount = currentCount
                else:
                    currentText = currentText[currentText.find(scanKey):len(currentText)]
                    currentCount = 0
            dict[scanKey] = biggestCount
            currentCount = 0
            biggestCount = 0
            
    for x in dict:
        values.append(str(dict[x]))
    isMatch = False  
    for j in range(1, lengthRows):
        if rows[j][1:length_of_row] == values:
            isMatch = True
            match = rows[j][0]
            break
    if isMatch == True:
        print(f"{match}")
    else:
        print("No match")
    fdna.close()
    fcsv.close()
main()
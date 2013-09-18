RomanNumeralValues = {"M":1000, "D":500, "C":100, "L":50, "X":10, "V":5, "I":1, "N":0}

def RomanToInt(roman):
    returnInt = 0
    stringCursor = 0

    while stringCursor < len(roman):
        if stringCursor == len(roman) - 1:
            returnInt += RomanNumeralValues[roman[stringCursor]]
        elif RomanNumeralValues[roman[stringCursor]] < RomanNumeralValues[roman[stringCursor+1]]:
            returnInt += (RomanNumeralValues[roman[stringCursor+1]] - RomanNumeralValues[roman[stringCursor]])
            stringCursor += 1
        else:
            returnInt += RomanNumeralValues[roman[stringCursor]]
        stringCursor += 1
    return returnInt

request = ""
while True:
    print "Input a Roman Numeral (or exit to quit)"
    request = raw_input("> ")
    if request != "exit":
        print request + " = " + str(RomanToInt(request))
    else:
        exit()

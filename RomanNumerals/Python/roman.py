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

def IntToRoman(integer):
    returnString = ""
    valueRemaining = integer
    validRomanCombinations = RomanNumeralValues.copy()

    #build up a list of valid roman combinations
    for key in RomanNumeralValues:
        for prefixKey in RomanNumeralValues:
            if prefixKey != "N":
                if IsPowerOfTen(RomanNumeralValues[prefixKey]):
                    if RomanNumeralValues[prefixKey] < RomanNumeralValues[key]:
                        if RomanNumeralValues[prefixKey] >= RomanNumeralValues[key]/10:
                            validRomanCombinations[prefixKey + key] = RomanNumeralValues[key] - RomanNumeralValues[prefixKey]
    
    #sort our list of valids by value, descending
    combos = sorted(validRomanCombinations.iteritems(), key=lambda item: -item[1])
    
    while valueRemaining > 0:
        #Find highest value (first in reverse sorted list) we can subtract
        for k,v in combos:
            if v <= valueRemaining:
                returnString += k
                valueRemaining -= v
                break
        
    return returnString

def IsPowerOfTen(integer):
    a = integer
    while a > 1:
        a = a/10
    return a==1

def IsInteger(inString):
    try:
        int(inString)
        return True
    except ValueError:
        return False

request = ""
while True:
    print "Input an integer, a Roman Numeral, or 'exit' to quit)"
    request = raw_input("> ")
    if request != "exit":
        if IsInteger(request):
            print request + " = " + IntToRoman(int(request))
        else:
            print request + " = " + str(RomanToInt(request))
    else:
        exit()


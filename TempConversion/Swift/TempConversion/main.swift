import Foundation

/**
Reads a line from standard input
- Parameter max: Specifies the number of bytes to read
- Returns: The string, or nil if an error was encountered trying to read Stdin
*/
func readln(max:Int = 8192) -> String? {
    assert(max > 0 && max < Int.max, "max must be between 1 and Int.max")
    
    var buf:Array<CChar> = []
    var c = getchar()
    while ((c != EOF) && (c != 10) && (buf.count < max)) {
        buf.append(CChar(c))
        c = getchar()
    }
    
    buf.append(CChar(0)) // null terminate
    
    return buf.withUnsafeBufferPointer { String.fromCString($0.baseAddress) }
}

/**
Gets regex capture groups from text
- Parameter pattern: The regex pattern
- Parameter text: The text to attempt to match
- Returns: A string array of captured groups. Index 0 is entire capture. Empty array if no match.
*/
func regexGroups(pattern: String!, text: String!) -> [String] {
    do {
        var collectMatches: [String] = []
        let re = try NSRegularExpression(pattern: pattern, options: [])
        let matches = re.matchesInString(text, options: [], range: NSRange(location: 0, length: text.utf16.count))
        if(matches.count == 0) {return []}
        let firstFullMatch = matches[0]
        
        for groupno in 0...re.numberOfCaptureGroups {
            let captureGroupRange = firstFullMatch.rangeAtIndex(groupno)
            collectMatches.append(
                (captureGroupRange.location == NSNotFound) ? "" :
                (text as NSString).substringWithRange(captureGroupRange))
        }
        return collectMatches
    }
    catch {
        return []
    }
}

let rgxTemperature = "(-)?(\\d+(\\.\\d+)?)(C|F|K)"
print("Enter temperature (ex. 99F, 23C, 213K): ", terminator: "")
let input = readln()!
let matches = regexGroups(rgxTemperature, text: input)

if(matches.isEmpty){
    print("You done goofed.")
    exit(EXIT_FAILURE)
}

let isNegative = (matches[1] == "-")
var tempValue = (matches[2] as NSString).floatValue
tempValue = (isNegative) ? -tempValue : tempValue
let unit = matches[4]

if(isNegative && unit == "K"){
    print("I refuse to acknowledge negative Kelvin.")
    exit(EXIT_FAILURE)
}

// Notice there's no fallthrough on Swift switches ☺
switch unit {
    case "K": //entucky
        print("\(tempValue) Kelvin is")
        print("\(tempValue - 274.15) Celsius")
        print("\((((tempValue - 274.15) * 9) / 5) + 32) Fahrenheit")
    case "F": //ried
        print("\(tempValue) Fahrenheit is")
        print("\(((tempValue - 32) * 5) / 9) Celsius")
        print("\((((tempValue - 32) * 5) / 9) + 274.15) Kelvin")
    case "C": //hicken
        print("\(tempValue) Celsius is")
        print("\(((tempValue * 9) / 5) + 32) Fahrenheit")
        print("\(tempValue + 274.15) Kelvin")
    default:
        print("I can't even.")
}
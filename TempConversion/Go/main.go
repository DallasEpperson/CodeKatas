package main

import "fmt"
import "strings"
import "strconv"
import "regexp"

func main() {
	re, _ := regexp.Compile("(-)?(\\d+(\\.\\d+)?)(C|F|K)")
	
	fmt.Print("Enter temperature (ex. 99F, 23C, 213K): ")
	var input string
	fmt.Scanln(&input)
	input = strings.ToUpper(input)
	
	valid := re.MatchString(input)
	if !valid {
		fmt.Println("You done goofed.")
		return
	}
	
	// FindStringSubmatch gets the groups from the matched regex
	// 0 = the whole thing
	// 1+ = the ordinal group
	res := re.FindStringSubmatch(input)
	
	if res[1] == "-" && res[4] == "K" {
		fmt.Println("Negative Kelvin? Are you trying to destroy the universe?")
	}
	
	degrees, _ := strconv.ParseFloat(res[2], 64)
	if res[1] == "-" {
		degrees = 0-degrees
	}
	
	switch res[4] {
		case "C":
			convertFromC(degrees)
		case "F":
			convertFromF(degrees)
		case "K":
			convertFromK(degrees)
	}
}

func convertFromC(c float64){
	fmt.Println(fmt.Sprintf("%g Celsius is", c))
	f := ((c * 9.0) / 5.0) + 32
	fmt.Println(fmt.Sprintf("%g Fahrenheit", f))
	k := c + 274.15
	fmt.Println(fmt.Sprintf("%g Kelvin", k))
}

func convertFromF(f float64){
	fmt.Println(fmt.Sprintf("%g Fahrenheit is", f))
	c := (f - 32) * (5.0/9)
	fmt.Println(fmt.Sprintf("%g Celsius", c))
	k := c + 274.15
	fmt.Println(fmt.Sprintf("%g Kelvin", k))
}

func convertFromK(k float64){
	fmt.Println(fmt.Sprintf("%g Kelvin is", k))
	c := k - 274.15
	fmt.Println(fmt.Sprintf("%g Celsius", c))
	f := ((c * 9.0) / 5.0) + 32
	fmt.Println(fmt.Sprintf("%g Fahrenheit", f))
}
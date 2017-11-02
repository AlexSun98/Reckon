Test 1:
Write some code that prints the numbers from 1 to 100. But for multiples of three print ¡°Boss¡± instead of the number and for the multiples of five
print ¡°Hog¡±. For numbers which are multiples of both three and five print ¡°BossHog¡±."


Test 2:
Preface:
Do not use any extended functionality of the framework to solve this problem. (For example, don't use the string finding methods of IndexOf,
Substring, Contains, regular expression classes, etc).
Problem:
We need a way of finding all the occurrences of a particular set of characters in a string. The set of characters can occur anywhere within the
string. The sample listed below should not be considered the only inputs and outputs
We need a class that has a public method named ¡°Find¡± that returns a string
This method accepts two strings as input. One called 'textToSearch' and one called 'subtext'
The solution should match the subtext against the textToSearch, outputting the positions of the beginning of each match for the subtext
within the textToSearch.
Multiple matches are possible
Matching is case insensitive
If no matches have been found, ¡°<No Output>¡± is generated

Sample Output
This is a sample subset of all possible inputs, there will be inputs not listed here that will have different outputs.
textToSearch = "Peter told me (actually he slurrred) that peter the pickle piper piped a pitted pickle before he petered out. Phew!"
Input subtext Expected Output
Peter 1, 43, 98
peter 1, 43, 98
Pick 53, 81
Pi 53, 60, 66, 74, 81
Z <No Output>
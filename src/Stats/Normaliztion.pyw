f = open('WeightsBest.txt')
s = f.read()
for i in range(len(s) - 2):
    if s[i + 1] == ',' and s[i] != 'f':
        s = s[:i + 1] + '.' + s[i + 2:]
f = open('WeightsBest.txt', 'w')
f.write(s)

# process from date 2012-05-26, try 5 days

import sys
import os

def getSolarTrace(skipLine, column, inputfile, outputfile):

	with open(inputfile, 'r') as f:
		lines_after_skip = f.readlines()[(int)(skipLine):]
		print 'slots in a day: ', len(lines_after_skip)
	
	with open(outputfile, 'a') as f2:
		for line in lines_after_skip:
			#print line
			items = line.split('	');
			#print 'len ', len(items)
			print >> f2, items[column]

if __name__ == '__main__':
	skipLine = 2
	column = 11
	inputfile = '..\\traces\\solarTrace\\ARC-2012-05-'
	outputfile = '..\\processedTrace\\solar.txt'

	if os.path.exists(outputfile):
		os.remove(outputfile)

	i = 0
	day = 26
	

	while i < 5:
		currinputfile = inputfile + '%d.txt' %(i+day)
		getSolarTrace(skipLine, column, currinputfile, outputfile)
		i += 1
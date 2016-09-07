#remove additive "_000X_" in filenames

import os
import sys

filenames=os.listdir(os.getcwd()) 

for num in range(len(filenames)):
	if filenames[num][-3:-1]=='.c' :
		print(filenames[num])

#!/usr/bin/python


#courses=['ART','PYTHON','MATH','SCIENCE']
#low_courses=[]
#for item in courses:
    #print(item)
 #   low_courses.append(item.lower())

#print(courses)
#print(low_courses)

#Item='art'.upper()
#print(Item)
#check_1=Item in courses
#print(check_1)
#list_1=['fg','gh','hj','kl']
#list_2=list_1 # it just point to old one 
#list_3=list_1.copy() #it create new list 
#print(list_1)
#print(list_2)
#print(list_3)
#list_2[0]='we'
#print(list_1)
#print(list_3)
# get vs [] for retrieving elements
#my_dict = {'name': 'Jack', 'age': 26}

# Output: Jack
""" print(my_dict['name'])

# Output: 26
print(my_dict.get('age'))

# Trying to access keys which doesn't exist throws error
# Output None
print(my_dict.get('address'))

if my_dict.get('hi') == None :
    print("true")
    print(0)
else:
    print("flase")    



# KeyError
#print(my_dict['address'])

 """
""" 
a = 5
b=90
c=80
print('The value of a = {0} and b = {2} and c = {1}'.format(a,b,c))

name =input("Enter youe full name:\n")
fir_part,sep,sec_part=name.partition(' ')
print(fir_part)
#print(sep)
#print(sec_part)
fir_sec,fir_sep,sec_sec=sec_part.partition(' ')
print(fir_sec)
#print(fir_sep)
print(sec_sec)

 """
""" 
x1 = 5
y1 = 5
x2 = 'Hello'
y2 = 'Hello'
x3 = [1,2,3]
y3 = [1,2,3]

# Output: False
print(x1 is  y1)

# Output: True
print(x2 is y2)

# Output: False
print(x3 is y3)
 """
""" x = 'Hello world'
y = {1:'a',2:'b'}

# Output: True
print('H' in x)

# Output: True
print('Hello'  in x)

# Output: True
print(1 in y)

# Output: False
print('a' in y)
 """
# Note: You may get different values for the id
""" 
a = 2
print('id(a) =', id(a))

a = a+1
print('id(a) =', id(a))

print('id(3) =', id(3))

b = 2
print('id(b) =', id(b))
print('id(2) =', id(2))
c=3
print('id(c) =',id(c))
 """
# Program to iterate through a list using indexing
""" 
genre = ['pop', 'rock', 'jazz']

# iterate over the list using index
for i in genre:
    print("I like", i)
 """
# program to display student's marks from record
""" student_name = 'Jules'

marks = {'James': 90, 'Jules': 55, 'Arthur': 77}

for student in marks:
    if student == student_name:
        print(marks[student])
        
else:
    print('No entry with that name found.')

 """

""" 
def absolute_value(num):
    # This function returns the absolute
    #value of the entered number

    #if num >= 0:
    #    return num
   # else:
   #     return -num


#ty=absolute_value(2)
#print(absolute_value.__doc__)
#print(ty)

#print(absolute_value(-4))

 """

""" 
def greet(*names):
    This function greets all
    the person in the names tuple.

    # names is a tuple with arguments
    #print(type(names)
    print(type(names))
    print(names.__len__())
    for name in names:
        print("Hello", name)


greet("Monica", "Luke", "Steve", "John",'Elkot')
 """

""" def outer():
    x = "local"
    global yu 
    yu ='hnvc gkv '
    def inner():
        nonlocal x
        x = "nonlocal"
        print("inner:", x)

    inner()
    print("outer:", x)
    print('Value of X = {0}'.format(x))

yu='ngbkkg'
print(yu)
outer()
print(yu) """


""" import imp



import example

sd = example.add(5,5)
print('sum = {}'.format(sd))
import example
import example
imp.reload(example)
import example
import example

print(dir()) """
""" 
import os 

print(os.getcwd())

os.chdir("c:\\Users\\m.ali")

print(os.getcwd())
print(os.listdir())
#os.mkdir('New_Elkot')
#os.rmdir('New_Elkot')

 """

 # program to print the reciprocal of even numbers
""" 
try:
    num = int(input("Enter a number: "))
    #assert num % 2 == 0
    raise(ValueError("Just try"))
except  :
    #print()
    print("Not an even number!")
else:
    reciprocal = 1/num
    print(reciprocal)

print ("the programe continue smooth")     """

""" 
import pyjokes

print(pyjokes)


 """

#######Debugging

import re

re

































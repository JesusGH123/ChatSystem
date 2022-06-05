import mysql.connector



import json

db_config_file_name = 'db_config.json.env'

db_config = json.loads(open(db_config_file_name).read())

db = db_config['db']
hst = db_config['hst']
usr = db_config['usr']
#pswd = db_config['pswd']

mydb = mysql.connector.connect(
  host=hst,
  user=usr,
  #password=pswd
)



mydb.cursor().execute(f'drop database if exists {db}')
mydb.cursor().execute(f'create database {db}')

import subprocess

with open('./chat.sql') as input_file:
  result = subprocess.run(['mysql',db, f'-h{hst}', f'-u{usr}'], stdin=input_file, capture_output=True)
  print(result)
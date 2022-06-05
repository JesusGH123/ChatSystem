1. Create file "db_config.json.env" with the following structure:
    {
        "db" : "chat"
        ,"usr" : ...
        ,"hst" : ...
    }
2. Run Python script "backup.py" every time you want to restore the data base contents based in "chat.sql" backup script
    -Disclaimer: you may need to install some python libraries like "mysql.connector". To install issue this command: pip install mysql-connector-python

CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255),
  instructions VARCHAR(500),
  img VARCHAR(500),
  category VARCHAR(255),
  creatorId VARCHAR(50),

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

ALTER TABLE recipes

DROP Table recipes;

INSERT INTO recipes 
(title, instructions, img, category, creatorId)
VALUES
('Buttered Toast', 'Get 2 slices of bread. Pop them in the toaster. Get your butter as much or as little as you like... Toast Pops and you can spread the butter on to the warm delicious toast. Now simply enjoy.', 'https://media.istockphoto.com/id/172413173/photo/toast-and-butter.jpg?b=1&s=170667a&w=0&k=20&c=KDDcmP7ezM7HmWjZfCmZDjCRGDEBVE359_YiI7T4bUQ=', 'Breakfast', '640538132afd6827951b3197')

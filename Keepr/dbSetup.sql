CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  isPrivate BIT NOT NULL,
  img TEXT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  img TEXT NOT NULL,
  views INT NOT NULL,
  kept INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT primary key,
  keepId INT NOT NULL,
  vaultId INT NOT NULL,
  FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
ALTER Table
  vaultKeeps
ADD
  creatorId VARCHAR(255) NOT NULL,
ADD
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE;
SELECT
  k.*,
  vk.id AS vaultKeepId,
  a.*
FROM
  keeps k
  JOIN vaultKeeps vk ON vk.keepId = k.id
  JOIN accounts a ON k.creatorId = a.id
WHERE
  k.id = 251;
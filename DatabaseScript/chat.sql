-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jun 08, 2022 at 04:26 AM
-- Server version: 5.7.36
-- PHP Version: 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `chat`
--

DELIMITER $$
--
-- Procedures
--
DROP PROCEDURE IF EXISTS `block_friend_ship`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `block_friend_ship` (`user_1` VARCHAR(20), `user_2` VARCHAR(20))  begin
  update friendship set is_blocked = 1 
  where
    (user_a = user_1 and user_b = user_2)
    or 
    (user_a = user_2 and user_b = user_1)
  ;
end$$

DROP PROCEDURE IF EXISTS `block_friend_ship_invite`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `block_friend_ship_invite` (`origin_user_` VARCHAR(20), `destination_user_` VARCHAR(20))  begin
  update friendship_request set is_blocked = 1 
  where
    (origin_user = origin_user_ and destination_user = destination_user_)
    or 
    (origin_user = destination_user_ and destination_user = origin_user_)
  ;
end$$

DROP PROCEDURE IF EXISTS `create_friendship`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `create_friendship` (`user_a` VARCHAR(20), `user_b` VARCHAR(20))  begin
  INSERT INTO friendship values (user_a, user_b, 0)
  ;
end$$

DROP PROCEDURE IF EXISTS `delete_friendship_invite`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_friendship_invite` (`origin_user_` VARCHAR(20), `destination_user_` VARCHAR(20))  begin
  update
    friendship_request
  set
    is_deleted=1
  where
    (origin_user = origin_user_ and destination_user = destination_user_)
    or 
    (origin_user = destination_user_ and destination_user = origin_user_)
  ;
end$$

DROP PROCEDURE IF EXISTS `delete_friend_ship`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_friend_ship` (`user_1` VARCHAR(20), `user_2` VARCHAR(20))  begin
  delete from friendship
  where
    (user_a = user_1 and user_b = user_2)
    or 
    (user_a = user_2 and user_b = user_1)
  ;
end$$

DROP PROCEDURE IF EXISTS `delete_message_for_all`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_message_for_all` (IN `user_a` VARCHAR(20), IN `user_b` VARCHAR(20), IN `timestamp_` TIMESTAMP)  begin
  update message set is_for_origin_deleted = 1 ,is_for_destination_deleted = 1
  where
    ((origin_user = user_a and destination_user = user_b)
    or 
    (destination_user = user_b and origin_user = user_a))
    and timestamp = timestamp_
  ;
end$$

DROP PROCEDURE IF EXISTS `does_this_friendship_invite_exist_and_not_blocked`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `does_this_friendship_invite_exist_and_not_blocked` (IN `origin_user_` VARCHAR(20), IN `destination_user_` VARCHAR(20))  select exists(
    select 
        * 
    from 
        friendship_request
    where 
      origin_user = origin_user_
      and destination_user = destination_user_
      and is_deleted = 0
      and is_blocked = 0
)$$

DROP PROCEDURE IF EXISTS `edit_message`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `edit_message` (IN `User_a` VARCHAR(20), IN `user_b` VARCHAR(20), IN `timestamp_` TIMESTAMP, IN `message` TEXT)  begin
  update message set text = message
  where
    ((origin_user = user_a and destination_user = user_b)
    or 
    (destination_user = user_b and origin_user = user_a))
    and timestamp = timestamp_
  ;
end$$

DROP PROCEDURE IF EXISTS `get_friendships_messages`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_friendships_messages` (IN `user_id` VARCHAR(20))  begin
  select 
    a.friend_id
    ,`source`
    ,`text`
    ,`timestamp`
	from
  (
    select 
      if(user_a = user_id,user_b,user_a) friend_id 
    from friendship 
    where 
      (user_a = user_id or user_b = user_id)
      and is_blocked = 0
  ) a left join (
    select 
      if(origin_user = user_id,destination_user,origin_user) friend_id
      ,if(origin_user = user_id,0,1) `source`  
      ,`text`
      ,`timestamp`
    from message 
    where
      (origin_user = user_id or destination_user = user_id)
      and
      not if(origin_user = user_id,is_for_origin_deleted,is_for_destination_deleted) 
    order by
      `timestamp`
  ) b
  on a.friend_id = b.friend_id
    ;
end$$

DROP PROCEDURE IF EXISTS `get_friendship_messages`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_friendship_messages` (IN `user_id_` VARCHAR(20), IN `friend_id_` VARCHAR(20))  begin
  select 
    `source`
    ,text
    ,timestamp
	from
  (
    select 
      if(user_a = user_id_,user_b,user_a) friend_id 
    from friendship 
    where 
      (user_a = user_id_ or user_b = user_id_)
      and (user_a = friend_id_ or user_b = friend_id_)
      and is_blocked = 0
  ) a left join (
    select 
      if(origin_user = user_id_,destination_user,origin_user) friend_id
      ,if(origin_user = user_id_,0,1) `source`  
      ,text
      ,timestamp
    from message 
    where
      (origin_user = user_id_ or destination_user = user_id_)
      and
      not if(origin_user = user_id_,is_for_origin_deleted,is_for_destination_deleted) 
    order by
      timestamp
  ) b
  on a.friend_id = b.friend_id
    ;
end$$

DROP PROCEDURE IF EXISTS `get_received_friendship_invites`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_received_friendship_invites` (`id_` VARCHAR(20))  begin
  select 
    b.id
    ,b.username
  from (
    select 
      origin_user
    from friendship_request
    where
      destination_user = id_
      and is_deleted = 0
      and is_blocked = 0
  ) a 
  left join
  (
    select 
      id
      ,username
    from 
      `user`
  ) b 
  on a.origin_user = b.id
  ;
end$$

DROP PROCEDURE IF EXISTS `insert_message`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `insert_message` (`message` TEXT, `from_user` VARCHAR(20), `to_user` VARCHAR(20))  begin
  INSERT INTO message values (from_user, to_user, 0, 0, message, NOW())
  ;
end$$

DROP PROCEDURE IF EXISTS `search_user_by_id_or_username`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `search_user_by_id_or_username` (`id_` VARCHAR(20), `username_` VARCHAR(30))  begin
  select 
   id
   ,username
  from user
  where
    if(trim(username_)!='',username like concat('%',username_,'%'),0)
    or
    if(trim(id_)!='',id like concat('%',id_,'%'),0)
  ;
end$$

DROP PROCEDURE IF EXISTS `send_friend_invite`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `send_friend_invite` (IN `my_id` VARCHAR(20), IN `candidate_id` VARCHAR(20))  begin
  insert into friendship_request values (my_id, candidate_id,0,0)
  ;
end$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `friendship`
--

DROP TABLE IF EXISTS `friendship`;
CREATE TABLE IF NOT EXISTS `friendship` (
  `user_a` varchar(20) NOT NULL,
  `user_b` varchar(20) NOT NULL,
  `is_blocked` tinyint(1) NOT NULL,
  PRIMARY KEY (`user_a`,`user_b`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `friendship`
--

INSERT INTO `friendship` (`user_a`, `user_b`, `is_blocked`) VALUES
('A3a4cC', 'abc3da', 0),
('A3a4cC', 'xXXFya', 1),
('A3a4cC', 'D0TjB5', 0),
('3aC34b', 'CasHByUWfeDR6uuVaBD4', 0),
('3aC34b', 'vTbGUM8DmXiDkHCfQ1pa', 0),
('3aC34b', 'jkwKHybp2BTmEsIfrCoS', 0),
('3aC34b', 'hiJ4gl92lHhExsMbqPxC', 0),
('CasHByUWfeDR6uuVaBD4', '4QrCmt9iDhNNnsYNySLI', 0),
('CasHByUWfeDR6uuVaBD4', 'k1YWfi0OO2yhJMlXFoeb', 0),
('BdAw5fTJHpSEwxUy0etd', 'k1YWfi0OO2yhJMlXFoeb', 0),
('BdAw5fTJHpSEwxUy0etd', 'CasHByUWfeDR6uuVaBD4', 0),
('BdAw5fTJHpSEwxUy0etd', 'vTbGUM8DmXiDkHCfQ1pa', 0),
('BdAw5fTJHpSEwxUy0etd', 'jkwKHybp2BTmEsIfrCoS', 0),
('BdAw5fTJHpSEwxUy0etd', 'hiJ4gl92lHhExsMbqPxC', 0),
('BdAw5fTJHpSEwxUy0etd', '4QrCmt9iDhNNnsYNySLI', 0),
('A3a4cC', '3aC34b', 0);

-- --------------------------------------------------------

--
-- Table structure for table `friendship_request`
--

DROP TABLE IF EXISTS `friendship_request`;
CREATE TABLE IF NOT EXISTS `friendship_request` (
  `origin_user` varchar(20) NOT NULL,
  `destination_user` varchar(20) NOT NULL,
  `is_blocked` tinyint(1) NOT NULL,
  `is_deleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`origin_user`,`destination_user`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `friendship_request`
--

INSERT INTO `friendship_request` (`origin_user`, `destination_user`, `is_blocked`, `is_deleted`) VALUES
('3aC34b', 'A3a4cC', 0, 0),
('xXXFya', 'A3a4cC', 1, 0),
('A3a4cC', 'abc3da', 0, 0),
('vTbGUM8DmXiDkHCfQ1pa', 'Eu3mu47mjYqx5BnLtanC', 0, 0),
('Mll66O5uRTXCt6JPKYQE', 'Eu3mu47mjYqx5BnLtanC', 0, 0),
('vTbGUM8DmXiDkHCfQ1pa', 'jkwKHybp2BTmEsIfrCoS', 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
CREATE TABLE IF NOT EXISTS `message` (
  `origin_user` varchar(20) NOT NULL,
  `destination_user` varchar(20) NOT NULL,
  `is_for_origin_deleted` tinyint(1) NOT NULL,
  `is_for_destination_deleted` tinyint(1) NOT NULL,
  `text` text NOT NULL,
  `timestamp` timestamp NOT NULL,
  PRIMARY KEY (`origin_user`,`destination_user`,`timestamp`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `message`
--

INSERT INTO `message` (`origin_user`, `destination_user`, `is_for_origin_deleted`, `is_for_destination_deleted`, `text`, `timestamp`) VALUES
('A3a4cC', '3aC34b', 0, 0, 'Qué onda misa. \r\n¿Falté a la reunión?', '2022-05-04 06:22:35'),
('abc3da', 'A3a4cC', 0, 0, 'Qué onda bro, vamos a cachorrear', '2022-05-25 19:21:46'),
('A3a4cC', '3aC34b', 0, 0, 'Hola, cómo estás?', '2022-05-25 19:41:16'),
('A3a4cC', '3aC34b', 0, 0, 'Qué onda bro, cómo estás?', '2022-05-25 19:45:24'),
('A3a4cC', '3aC34b', 0, 0, 'Qué onda bro, cómo estás?', '2022-05-25 19:46:04'),
('A3a4cC', '3aC34b', 0, 0, 'Qué onda bro, cómo estás?', '2022-05-25 19:46:29'),
('A3a4cC', '3aC34b', 1, 1, 'mensaje editado', '2022-05-25 19:47:34');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `username` varchar(30) NOT NULL,
  `id` varchar(20) NOT NULL,
  `password` varchar(64) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`username`, `id`, `password`) VALUES
('chucho', 'A3a4cC', 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'),
('misa', '3aC34b', 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'),
('test', 'abc3da', 'aaa'),
('Username', 'dfdcd', 'Password'),
('Username', 'xXXFya', 'Password'),
('Username', 'D0TjB5', 'Password'),
('Username', 'k1YWfi0OO2yhJMlXFoeb', 'Password'),
('Username', '4QrCmt9iDhNNnsYNySLI', 'Password'),
('Username', 'NLYdKT4HUz2wEMgjWiia', 'Password'),
('Mike Muñoz', 'Mll66O5uRTXCt6JPKYQE', 'abc'),
('Mike Muñoz', 'BdAw5fTJHpSEwxUy0etd', '1234'),
('Miguel Ángel Muñoz Vázquez', 'vTbGUM8DmXiDkHCfQ1pa', '1234'),
('Mike Muñoz', 'jkwKHybp2BTmEsIfrCoS', 'abc'),
('Mike Muñoz', 'PCZKmcFI0OWbpCSVq8QA', 'abc'),
('Mike Muñoz', 'hiJ4gl92lHhExsMbqPxC', 'abc'),
('Mike Muñoz', 'CasHByUWfeDR6uuVaBD4', 'abc'),
('Miguel Ángel Muñoz Vázquez 2', 'Eu3mu47mjYqx5BnLtanC', '1234');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

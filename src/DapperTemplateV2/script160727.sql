drop table if exists UrlInfo;

/*==============================================================*/
/* Table: UrlInfo                                               */
/*==============================================================*/
create table UrlInfo
(
   id                   bigint not null auto_increment,
   url                  varchar(1000),
   urlMd5               varchar(50),
   shortVal             varchar(20),
   comment              varchar(1000),
   state                int comment 'state=1 ���� =2���Ӷ̵�ַ',
   createTime           datetime,
   isDel                int,
   primary key (id)
);

alter table UrlInfo comment '��ַ��Ϣ';


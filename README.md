# mvcprocpage
基于mvc实现大数据量分页

有时候大数据量进行查询操作的时候，查询速度很大强度上可以影响用户体验，因此自己简单写了一个demo，简单总结记录一下：

技术：Mvc4+Dapper+Dapper扩展+Sqlserver

目前主要实现了两种分页：一种采用 PagedList.Mvc 实现的分页

两外一种采用 ajax异步加载分页 采用比较常用的jquery.pagination 分页插件。

功能相对比较简单仅供学习交流。
详细： http://www.cnblogs.com/hgmyz/p/6753050.html

## EasyRBAC的部署

### 准备
1、MySQL （建议5.7+）
2、dotnet core 2.0
3、Node.js 7+

### 私货
这个项目里有我个人的两个私货：[SQLinq](https://github.com/uliian/SQLinq) ，[MyUtility](https://github.com/uliian/MyUtility) 虽然它们是私货，但是都开源，可以放心用，用得不开心可以给我issue或者PR。

[SQLinq](https://github.com/uliian/SQLinq)是一个运行时SQL生成器，一个分支项目，原项目貌似已经不维护了（我提了pr都不理我的），我fork了它，主要增加了对MySQL的支持以及增加了一些功能。说句实话，这个项目的组织方式和代码结构以及一些使用方式我是不满意的，但是谁让我懒呢？你们要是也觉得哪里很难受，可以给我提Issue，被人关注了难说我就有动力去改进了。

[MyUtility](https://github.com/uliian/MyUtility)是一堆我自己常用的辅助类。其中我个人认为比较有价值的是一个类`Snowflake`的ID生成器，这个生成器性能极高，比较有逼格的地方在于无悲观锁设计。所有的锁相关的操作都使用了CAS操作。大家有需求兴趣可以撸了去用。

## 数据库

[建库脚本](../easyRBAC/DbScript.sql) 

注意：如果您的MySQL版本低于5.7，建库过程中很可能会出现以下错误：
>#1071 - Specified key was too long; max key length is 767 bytes

这个问题的讨论可以参见[这里](https://stackoverflow.com/questions/1814532/1071-specified-key-was-too-long-max-key-length-is-767-bytes)

你完全可以将报告的过长的字段长度缩减至满足条件即可。在绝大多数情况下不影响使用，只会导致你的目录层级减小一丢丢而已。

## 后端程序

本应用使用了前后端分离架构，并且后端API开放了`CORS`。所以前后端不在同一域名下毫无问题。后端代码即.NET Core代码，相信大家都可以很快找到，并编译成功。

如果有问题很可能会是包还原的问题，因为我刚才提到的两个私货都是放在我的MyGet上而不是放在Nuget上，所以可能会比较慢。

## 前端程序

即`easyRbAC-UI`目录下的项目。采用Vue+Element编写。你可以在目录下执行

```bash
npm install
npm run dev
```

就可以成功启动UI项目。

ps:有个小坑，因为鄙人实在webpack技术烂到家，导致SSO.HTML如果通过npm build之后build的东西完全没办法用，如果需要部署，请把sso.html直接复制粘贴到项目里

## 运行程序

先运行后端程序，再进入前端站点`http://127.0.0.1:8010/` 会自动跳转到SSO页面，默认用户名：`admin` 密码： `888888aa`
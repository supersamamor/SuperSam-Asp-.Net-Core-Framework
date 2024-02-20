Publish to private Nuget server

nuget push Filinvest.Common.Configuration.2.0.0.nupkg D87DEF8547B991862D411152F57AC -Source http://fai-ecrm-as01:8082/api/v2/package

Delete package from private Nuget server
nuget delete Filinvest.Common.Configuration 2.0.0 -apikey D87DEF8547B991862D411152F57AC -Source http://fai-ecrm-as01:8082/api/v2/package
- Release 0.1 Updates cshtml props with razorgenerator settings - but not the most effective approach since it generates redundant .generated.cs files
- So, in case of 0.2 release, have the provision to migrate from RazorGenerator.Mvc to RazorGenerator.MsBuild nuget package and the associated cleanup process
- Could save someone their time since I didn't know initially that RazorGenerator.MsBuild is cleaner....

0.4 release:
Converts cshtml to precompiled views: RazorGeneratorUtility.exe 7 sln path grand par path

# RazorGeneratorUtility
- Release 0.1 Updates cshtml props with razorgenerator settings - but not the most effective approach since it generates redundant .generated.cs files
- So, in case of 0.2 release, have the provision to migrate from RazorGenerator.Mvc to RazorGenerator.MsBuild nuget package and the associated cleanup process
- Could save someone their time since I didn't know initially that RazorGenerator.MsBuild is cleaner....

Prerequisites:
Needs a packages.config for each csproj

Known issues:
- packages folder location is hard-coded and that relative location might have to be fine-tuned depending on your proj location

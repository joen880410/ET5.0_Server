cd %cd%/Publish/win-x64

dotnet App.dll --appId=2 --appType=Benchmark --config=../../Config/StartConfig/Benchmark.txt --configOther=../../Config/{0}.txt
pause
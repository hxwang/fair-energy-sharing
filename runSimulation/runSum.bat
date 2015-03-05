
:FOR %%G IN (0.1 0.2 0.3 0.4 0.5 0.6 0.7 0.8 0.9 1 1.1 1.2 1.3 1.4 1.5 1.6 1.7 1.8 1.9 2.0) DO ..\fair-energy-sharing\bin\Debug\fair-energy-sharing.exe -t 1440 -r 30 -hc 100 -per 0.75 -ratio %%G
:pause




FOR %%G IN (1) DO ..\fair-energy-sharing\bin\Debug\fair-energy-sharing.exe -t 1440 -r 1 -hc 100 -per 0.75 -ratio %%G
pause

echo off

set /p day=What number day should we add? 

echo "" > .\inputs\day%day%.txt
echo namespace Advent { >> .\Day%day%.cs
echo	internal class Day%day% : ISolution { >> .\Day%day%.cs
echo public Day%day%() { >> .\Day%day%.cs
echo }}} >> .\Day%day%.cs

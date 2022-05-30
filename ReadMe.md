### Preconditions:
- .NET Framework 6.0

### In order to run tests:
1. Build solution
2. Install Chrome and Firefox latest version on your OS
3. In Test Explorer choose tests to run
4. When Tests are finished the Report with Test Results could be found in [bin] folder of Solution

P.S.
Although Test Framework is setup to be able to run Tests in parallel is 2 threads 
it is strongly recommended to run tests one by one, since the Website under test is not capable of handling several threads and is constantly down
giving "Resource limit is reached" error.
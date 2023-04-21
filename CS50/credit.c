#include <stdio.h>
#include <cs50.h>


long power();


int main(void)
{
    //AMERX 15 STRT 34 OR 37
    //MC 16 STRT 51, 52, 53, 54, 55
    //VZA 13 OR 16 STRT 4

   long input;
   bool isValid = true;
   string type;
   int mod = 10;
   int place = 1;
   int num = 0;
   int totalEven = 0;
   int totalOdd = 0;

   do
   {
      input = get_long("Number: ");
      

   }

   while(input < 0);
   
      
      for(int i = 0; i < 16; i++)
      {
          num = ((input % mod) - (input % (mod /10))) / (mod/10);
          if(place % 2 == 0)
          {
              totalEven = totalEven + num * 2;
          }
          else
          {
             totalOdd = totalOdd + num; 
          }
          
          mod = mod * 10;
          place++;
      }
      
      if((totalEven + totalOdd) % 10 == 0 )
      {
          isValid = true;
      }
      else
      {
          isValid = false;
          
          printf("%i , %i", totalEven, totalOdd);
      }
   if (isValid == true){
   if((input < (38 * power(10, 13)) && input >=(37 * power(10, 13))) || (input < (35 * power(10,13)) && input >= (34 * power(10, 13)))) 
   {
       type = "AMEX\n";
   }
   else if(input >= (51 * power(10, 14)) && input < (56 * power(10, 14)))
   {
       type = "MASTERCARD\n";
   }
   else if((input < (5 * power(10, 12)) && input >= (4 * power(10, 12))) || (input < (5 * power(10, 15)) && input >= (4 * power(10, 15))))
   {
       type = "VISA\n";
   } 
       else{
       type = "error\n";
   }
}
   else
   {
       type = "INVALID\n";
   }
   

    if((totalEven + totalOdd) % 10 == 0 )
    {
      isValid = true;
    }
    else
    {
      isValid = false;
    }
      
    printf("%s", type);

}





long power(int n, int p)
{
    long result = 1;
    for(int i = 0; i<p; i++)
    {
        result = result * n;
    }
    return result;
import java.util.Random;


public class SqrMapGenerator {

 int width = 30;
 int height = 20;
 public int map[][];
 public int msqrs[][];
 
 int fillPercent = 45;
 int SEED = 170591;
 
 public static void main(String[] args) {
  SqrMapGenerator sqrMap = new SqrMapGenerator();
  sqrMap.init();
  //sqrMap.hardcode();
  sqrMap.smooth();
  //sqrMap.printMap();
  sqrMap.marchSquares();
  sqrMap.printConsole();
 }
 
 public SqrMapGenerator(){}
 
 public SqrMapGenerator(int width, int height, int fillPercent, int seed) {
  this.width = width;
  this.height = height;
  this.fillPercent = fillPercent;
  SEED = seed;
 }

 void init(){
  Random rand = new Random(SEED);
  
  map = new int[height][width];
  msqrs = new int[height-1][width-1];
  
  for(int i=0; i<height; i++){
   for(int j=0; j<width; j++){
    if(i==0 || i==height-1 || j==0 || j==width-1){
     map[i][j] = 1;
    }else{
     int random = rand.nextInt(100);
     if(random <= fillPercent){
      map[i][j] = 1; 
     }else{
      map[i][j] = 0;
     }
    }
   }
  }
 }
 
 void smooth(){
  for (int x = 0; x < height; x ++) {
   for (int y = 0; y < width; y ++) {
    int neighbourWallTiles = getNeighbourWallCount(x,y);

    if (neighbourWallTiles > 4)
     map[x][y] = 1;
    else if (neighbourWallTiles < 4)
     map[x][y] = 0;

   }
  }
 }
 
 int getNeighbourWallCount(int x, int y){
  int wallCount = 0;
  
  for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX ++) {
   for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY ++) {
    if (neighbourX >= 0 && neighbourX < height && neighbourY >= 0 && neighbourY < width) {
     if (neighbourX != x || neighbourY != y) {
      wallCount += map[neighbourX][neighbourY];
     }
    }
    else {
     wallCount ++;
    }
   }
  }
  return wallCount;
 }
 
 public void marchSquares(){
  for(int i=0; i<height-1; i++){
   for(int j=0; j<width-1; j++){
    msqrs[i][j] = getBinaryWalls(i,j);
   }
  }
 }
 
 int getBinaryWalls(int x, int y){
  byte binary = 0x0; //0000
  
  for (int neighbourX = x ; neighbourX <= x + 1; neighbourX ++) {
   for (int neighbourY = y ; neighbourY <= y + 1; neighbourY ++) {
    if (neighbourX > 0 && neighbourX < height-1 && neighbourY > 0 && neighbourY < width-1) {
     if(neighbourX <= x && neighbourY <= y && map[neighbourX][neighbourY]==1 ){
      binary |= 0x1;
     }else if(neighbourX <= x && neighbourY > y && map[neighbourX][neighbourY]==1 ){
      binary |= 0x2;
     }else if(neighbourX > x && neighbourY > y && map[neighbourX][neighbourY]==1 ){
      binary |= 0x4;
     }else if(neighbourX > x && neighbourY <= y && map[neighbourX][neighbourY]==1 ){
      binary |= 0x8;
     }
    }else{
     if (neighbourX <= 0){
      binary |= 0x3;// 0001 | 0010
     }else if (neighbourX >= height-1){
      binary |= 0xC;// 1000 | 0100
     }
     if(neighbourY <= 0){
      binary |= 0x9;// 1000 | 0001 
     }else if(neighbourY >= width-1){
      binary |= 0x6;// 0100 | 0010 
     }
    }
   }
  }
  return binary;
 }
 
 public void printConsole(){
  for(int i=height-2; i>=0; i--){
   for(int j=0; j<width-1; j++){
    switch(msqrs[i][j]){
    case 0 : System.out.print(" "); break;
    case 1 : System.out.print("\\"); break;
    case 2 : System.out.print("/"); break;
    case 3 : System.out.print("_"); break;
    case 4 : System.out.print("\\"); break;
    case 5 : System.out.print("/"); break;
    case 6 : System.out.print("|"); break;
    case 7 : System.out.print("/"); break;
    case 8 : System.out.print("/"); break;
    case 9 : System.out.print("|"); break;
    case 10 : System.out.print("\\"); break;
    case 11 : System.out.print("\\"); break;
    case 12 : System.out.print("_"); break;
    case 13 : System.out.print("/"); break;
    case 14 : System.out.print("\\"); break;
    case 15 : System.out.print("X"); break;
    }
     /*if(msqrs[i][j] > 9){
       System.out.print("["+msqrs[i][j]+"]");
     }else{
       System.out.print("[0"+msqrs[i][j]+"]");
     }*/
   }
   System.out.print("\n");
  }
 }
 
 void printMap(){
  for(int i=height-1; i>=0; i--){
   for(int j=0; j<width; j++){
    if(map[i][j] == 1){
     System.out.print("X"); 
    }else{
     System.out.print(" ");
    }
   }
   System.out.print("\n");
  }
 }
 
 void hardcode(){
  map = new int[height][width];
  msqrs = new int[height-1][width-1];
  
  map[0][0] = 1;
  map[0][1] = 1;
  map[0][2] = 1;
  
  map[1][0] = 1;
  map[1][1] = 0;
  map[1][2] = 1;
  
  map[2][0] = 1;
  map[2][1] = 1;
  map[2][2] = 1;
 }

}

/*#pragma strict

enum Direction {north, south, east, west};

var vert : boolean;
var maxAmount : float; 
var dir : Direction;
var dir2 : Direction;
var maxY : float;
var minY : float;
var stepY : float;
var stepX : float;
var maxX : float;
var minX : float;

function Start () {
		dir = Direction.north;
	
		dir2 = Direction.east;

}

function Update () 
{
	if( vert)
	{

		if (dir == Direction.north)
		{
			if(transform.position.y > maxY)
			{
				dir = Direction.south;
				stepY *= -1;
			}
		}
			
		if(dir == Direction.south)
		{
			if (transform.position.y < minY)
			{
				dir = Direction.north;
				stepY *= -1;
			}
			
		}
			
		transform.position.y += stepY;
	}
	
	else
	{
	
		if (dir2 == Direction.east)
		{
			if(transform.position.x > maxX)
			{
				dir2 = Direction.west;
				stepX *= -1;
			}
		}
			
		if(dir2 == Direction.west)
		{
			if (transform.position.x < minX)
			{
				dir2 = Direction.east;
				stepX *= -1;
			}
		}
			
		transform.position.x += stepX;
	}
}
*/
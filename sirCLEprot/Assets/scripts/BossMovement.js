#pragma strict

enum Direction {north, south, east, west};

var maxAmount : float; 
var dir1 : Direction;

var maxY : float;
var minY : float;
var stepY : float;
var stepX : float;
var maxX : float;
var minX : float;

function Start () {
		dir1 = Direction.north;

}

function Update () 
{


		if (dir1 == Direction.north)
		{
			if(transform.position.y > maxY)
			{
				dir1 = Direction.west;
				stepX *= -1;
			}
		}
		
		transform.position.y += stepX;
			
		if(dir1 == Direction.west)
		{
			if (transform.position.x < minX)
			{
				dir1 = Direction.south;
				stepY *= -1;
			}
			
		}
			
		transform.position.x += stepX;
		
		
		if(dir1 == Direction.south)
		{
			if(transform.position.y < maxY)
			{
			dir1 = Direction.east;
			stepX *= -1;
			}
		}
		
		transform.position.y += stepY;
		
		if(dir1 == Direction.east)
		{
			if(transform.position.x < minX)
			{
			dir1 = Direction.south;
			stepY *= -1;
			}
		}
		
		transform.position.y += stepY;
				
};
	
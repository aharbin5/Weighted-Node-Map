### Weighted-Node-Map ###
A simple object-oriented node map to test and generally understand path-finding algorithms better.

## How to use ##
I'll go through and explain the Map Object & Its functions, The Node Object & Its functions, & why I chose to write and post such terrible code.
#Map object #
The map object can hold a maximum of 20 nodes.  This number is arbitrarily set and I could've avoided this problem had I just used Lists.  I might switch over later, who knows? Surely not me.

__void Map.createNode(string Name)__
  This function checks for space in the nodeList and if it's available, creates a new Node object and places it in.
  Rememeber the name you set, it's the onl way you can directly reference the object once its in the array.
  ~You know I was going to use IDs for this but decided not to right as I started.  That decision was a mistake~

__void Map.connectNode(string station1Name, station2Name, int pathWeight)__
  This function establishes a connection between two Node objects by adding each others reference to the Node's "connections" array.
  This creates the connection on both ends, so no need to write a "connectNode()" for each direction.

__string Map.findCapacity()__
  This function doesn't serve *much* of a purpose, but it does act as a verification that your nodes were put into the array with no errors.
  
__int getIndex(string stationName)__
  This is an ease of use function so I don't have to write the loop to find the Node name in the list every time.
  Once again, this could have been avoided if I just used IDs instead of names.
  ~Once again, once again, that's a later problem and it's 2am.~

__string pathTo(string statingStation, string endStation)__
  So this function was kind of the entire purpose of building this whole thing.  I wanted to write a pathing algorithm and didn't realize how difficult the implementation was after reading 4-5 articles about the theory.  
  Reguardless of my delusions of mediocrity, this algorithm is by no means good, efficient, or even correct.  However, in the tests I did with my example map, it could navigate to every station from every other station.
  I am aware it usually takes the second fastest path or something similar, but like most things in life, it's a later problem.

__override ToString()__
  Oh this one's the most important of all.  This function will print your entire map sorted by the Nodes in the order you put them in.  
  They have lines to denote the Node names then the connections are tabbed out under them.  
  Ex:
  -- Example Staton/Node --
    0: Next Station/Node, 3
    1: Last Station/Node, 9

# Node Object #
The node object is really simple.  It's just the name, an array of other nodes, & the weights of those connections.

__void addNewConnection(Node newNode, int newWeight)__
  This function is a simple call that just checks for a space and inputs the weight and node into the same index in their respective arrays.
  
__override ToString()__
  This ToString returns the connections and pathweights tabbed out as their shown in the Map.ToString()
  Ex: "0: Example Station/Node, 5"

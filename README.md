# NetML
Windows GUI for the NS3 network simulation system.

Current version is missing a lot of features, non terrestrial networks do not function, however wired networks are implemented.

# Installation
1. Install Cygwin and NS3 as per [this tutorial](http://www.jasonernst.com/2010/04/15/ns-3-7-1-windows-7-cygwin/). If you need graph output you will also have to select gnuplot under graphics when installing Cygwin.
2. Copy the src folder in the NS3 Modifications folder and overwrite the src folder in the version of NS3 you installed. If you installed a version other than NS3.22 you will need to instead copy the .h and .cc file and modify the wscript file to include them manually.
3. When launching NetML for the first time it will ask for the locations of the NS3 directory and the bash executable location.

# Use
## Main Window
* Left clicking creates a node.
* Right clicking creates a domain - however domains are not yet implemented.
* Right clicking and dragging creates a link between nodes. This is the physical connection data travels over.
* Middle clicking and dragging creates a stream between nodes. This is the server/client data that will be routed over the network.
* Left clicking and dragging moves all elements.
* Right clicking an element will open a context menu.

## Traces
Traces are what are used to measure details about the network. Each trace will store a integer value and output whenever the value is changed.

## Plots
Plots are what are used to create a visual representation of the data from traces. Each plot contains data from and number of traces.

## Network Parameters
Here you can specify some details about the network to be simulated. Add modules if you want output from the various NS3 components during the simulation of the network.

## Display Properties
Here you can select which attribute of the elements is used for rendering, these settings also affect what is show in trace source combo boxes.

## Run Simulation
This will open up a console window which will show output from the process of creating the C++ program, compiling it under Cygwin, running the simulation, averaging data, and generating plots. After the simulation is finished you can use the textbox to run any other commands under bash if required. After the simulation is completed output files *should* be copied to the networks save folder however in some cases plots do not get copied - in this case you will find them in your NS3 folder under build/scratch/out.

# Screenshots
Example graph output.
![Example graph](http://52.62.164.10/image/20160606021033968.png)

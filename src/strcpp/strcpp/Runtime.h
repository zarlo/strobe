#include "Memory.h"
#include "DIFExecutable.h"
#pragma once
using namespace str::dif;
namespace str
{
	namespace runtime
	{
		// The runtime class
		class Runtime
		{
		public:
			// The memory
			Memory *memory;

			// Processes
			PArray *processes;

			// Labels
			LArray *labels;

			// Initialize the runtime
			Runtime(int);

			//  Add a new process
			void Start(Executable);

			// Step once in every process
			void Step();

		private:
			// Process the instruction
			void Work(Instruction);

			// Initialize reserved
			void Reserved(int);
		};

		// Label
		typedef struct
		{
			int ID;
			int Location;
		} cLabel;

		// Label Array
		typedef struct
		{
			cLabel *value;
			int size;
			int current;
		} LArray;

		// Process
		typedef struct
		{
			IArray inst;
			bool running;
			int current;

			// Step in the process
			Instruction Step()
			{
				if (current >= inst.size)
				{
					throw(false);
				}
				current++;
				return inst.value[current];
			}

		} Process;

		// Process Array
		typedef struct
		{
			Process *value;
			int size;
			int current;
		} PArray;
	}
}
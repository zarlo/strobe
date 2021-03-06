#pragma once
#include "Common.h"
#include "Instruction.h"
using namespace std;
namespace str
{
	// The variable structure
	class Variable
	{
	public:
		int id;
		vector<Byte> content;

		// Initialize
		Variable(int size,int newID)
		{
			content.resize(size);
			id = newID;
		}

		// Change the content to the value
		void set(vector<Byte> newC)
		{
			content.resize(newC.size());

			for (int i = 0; (size_t)i < newC.size(); i++)
			{
				content[i] = newC[i];
			}
		}
	};

	// The memeory manager class
	class Memory
	{
	public:
		vector<Variable> variables;

		// Allocate a new variable
		void alloc(Variable var)
		{
			variables.push_back(var);
		}

		// Remove a variable from id
		void remove(int id)
		{
			for (int i = 0; (size_t)i < variables.size(); i++)
				if (variables[i].id == id)
				{
					variables.erase(variables.begin() + i);
					return;
				}
		}

		// Get value
		vector<Byte> get(int id)
		{
			for (int i = 0; (size_t)i < variables.size(); i++)
				if (variables[i].id == id)
					return variables[i].content;
			vector<Byte>x;
			x.push_back(0);
			return x;
		}

		// Get the size
		size_t size()
		{
			return variables.size();
		}

		// Assign bytes to a variable
		void assign(int id, vector<Byte> newC)
		{
			for (int i = 0; (size_t)i < variables.size(); i++)
				if (variables[i].id == id)
				{
					variables[i].set(newC);
					return;
				}
		}

		// Replace the variable content with single byte 0
		void clear(int id)
		{
			for (int i = 0; (size_t)i < variables.size(); i++)
				if (variables[i].id == id)
				{
					vector<Byte> empty;
					empty.push_back(0);
					variables[i].set(empty);
					return;
				}
		}

	};

	// The Runtime Class
	class Runtime
	{
	private:
		Memory memory = Memory();
	
		// Execute the instruction
		void Execute(Instruction inst)
		{
			vector<int> args = ParseArgs(inst.parameters);
			switch (inst.operation)
			{
				// Move Contents 
			case Op_Move:
				memory.assign(args[0], memory.get(args[1]));
				break;
				// Allocate
			case Op_Allocate:
				memory.alloc(Variable(args[1], args[0]));
				break;
				// Assign
			case Op_Assign:
				memory.assign(args[0], GetSecond(inst.parameters));
				break;
				// Op Code: Test (Memory Dump)
			case Op_Test:
				cout << "== DUMP START ==\n";
				for (int i = 0; (size_t)i < memory.size(); i++)
					cout << memory.variables[i].id << ": " << memory.variables[i].content.size() << "\n";
				cout << "== DUMP END ==\n";
				break;
				// Not implemented yet
			default:
				throw("Not implemented yet.");
				break;
			}
		}

	public:
		// Run an executable
		void Run(Executable e)
		{
			for (int i = 0; (size_t)i < e.insturctions.size(); i++)
				Execute(e.insturctions[i]);
		}
	};
}
# AT28C64B EEPROM Programmer — Arduino Sketch & Hardware Flasher

## System Overview & Objectives

This project implements a custom hardware programmer driven by an Arduino board (such as the Uno or Nano) to interface directly with the Microchip/Atmel AT28C64B 64K (8K x 8) Parallel EEPROM. By utilizing shift register pin expansion, the circuit minimizes the required microcontroller I/O pins, allowing standard Arduino boards to read, flash, dump, and verify the complete 8KB address space of the memory chip efficiently.

### Key Capabilities

- **Full Address Coverage:** Accesses all 8,192 bytes (0x0000 to 0x1FFF) using a daisy-chained serial-to-parallel shift architecture.
- **Software Data Protection (SDP):** Built-in logic to execute byte-write sequences and SDP disable/enable algorithms.
- **Hex Memory Dump:** Outputs full memory maps in standardized 16-byte hex format over serial for inspection and verification.
- **Verification Logic:** Automated post-write verification against source image arrays to ensure silicon state integrity.

## Hardware Architecture & Pin Expansion

Because the AT28C64B requires 13 address lines (A0–A12), 8 data lines (IO0–IO7), and 3 control lines (~CE, ~OE, ~WE), driving it directly would exceed the available GPIO on standard Arduino platforms. This design uses pin expansion techniques to streamline hardware demands.

### Hardware Components

- **Driving Controller:** Arduino Uno / Nano (ATmega328P).
- **Address Bus Expansion:** Dual daisy-chained 74HC595N 8-bit shift registers. These convert 3 serial pins from the Arduino (Data, Clock, Latch) into a 13-bit parallel address bus vector (with 3 extra bits reserved for system expansion).
- **Control & Data Lines:** Direct GPIO connections for line-level control over Write Enable (~WE), Chip Enable (~CE), Output Enable (~OE), and the bi-directional 8-bit Data Bus.

### Pin Interconnect Mapping

| Module Pin | Arduino Pin | Function |
| --- | --- | --- |
| **74HC595 DS** | Pin 2 | Serial Data Input for Address Bus |
| **74HC595 SH_CP** | Pin 3 | Shift Register Clock |
| **74HC595 ST_CP** | Pin 4 | Storage Register Clock (Latch) |
| **EEPROM ~WE** | Pin 13 | Write Enable (Active Low) |
| **EEPROM ~OE** | Pin 12 | Output Enable (Active Low) |
| **EEPROM ~CE** | Pin 11 | Chip Enable (Active Low) |
| **Data Bus D0–D7** | Pins 5–10, A0, A1 | Bi-directional Data Lines |

## Workflow & Operations

### 1. Flashing EEPROM Maps

Writing data to the AT28C64B requires cycling through address states, placing data on the 8-bit bus, and toggling ~WE while holding ~CE low and ~OE high.

- **Shift Address:** Shift the 13-bit address out via the 74HC595 registers and latch it to the output pins.
- **Set Data Bus:** Configure Arduino data pins to `OUTPUT` mode and set the byte payload.
- **Pulse ~WE:** Drive ~WE LOW for a duration matching specs (`tWP` ≥ 100 ns), then return HIGH.
- **Write Cycle Delay:** Allow internal write timing (`tWC` ≤ 10 ms) to complete before initiating subsequent operations.

### 2. Verification & Memory Dumps

To confirm that burned memory contents match expected binaries, the programmer features a serial memory read utility.


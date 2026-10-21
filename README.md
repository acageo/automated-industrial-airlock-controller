# Automated Industrial Airlock Controller — FSM EEPROM Architecture

## Executive Overview
This repository hosts the full engineering lifecycle for an **Automated Industrial Airlock Controller** implemented via a hardware-based **Finite State Machine (FSM)** stored on EEPROM silicon. The system is designed for high-reliability cleanroom and hazardous area control, enforcing strict interlocking policies between dual-door access points, ventilation cycles, and emergency overrides.

The system is built around a Finite State Machine (FSM) architecture, utilizing an EEPROM as the core combinational logic driver to sequence airlock states securely and predictably.

The workspace is structured into three primary sub-projects:
* **Offline Validator:** A high-speed simulation tool written in C# to rigorously exercise the state transition matrix against deterministic edge cases before physical flashing.
* **Blackbox Validator:** An automated test harness that drives physical microcontrollers or hardware-in-the-loop (HIL) simulators to monitor real-time FSM timing, boundary conditions, and line transitions.
* **EEPROM Programmer:** A specialized flashing utility and hardware interface layer used to translate verified FSM truth tables into binary images and program target EEPROM chips (e.g., AT28C64 / AT28C256).

## FSM Operational & Interlocking Logic
The airlock state transition matrix enforces a strict physical safety policy based on priority-encoded control signals. During normal operation, valid transition requests (e.g., requesting Door A or Door B access) step the system sequentially through pressurization, door lock release, and venting cycles; however, if conflicting signals are asserted simultaneously—such as pushing unlock buttons for both Door A and Door B at the same time—the EEPROM logic evaluates the condition as an illegal state transition and holds the system safely in place with no state change. The system's override hierarchy ensures that critical safety conditions take strict precedence: asserting the Emergency (SOS) signal instantly overrides all normal operations and forces the FSM into an isolated emergency lock state. The Service/Maintenance signal occupies the highest operational priority, overriding both standard requests and active Emergency states. Toggle-pulsing the Service signal acts as the master manual reset, allowing authorized personnel to securely clear active maintenance modes as well as restore the FSM back to the default Idle state following an emergency lockdown.

# Repository Architecture & Directory Structure
```
automated-industrial-airlock-controller/
├── src/                        # C# source & firmware projects
│   ├── eeprom-programmer/      # Arduino flash utility & shift register logic
│   ├── offline-validator/      # C# fast-simulation state matrix test suite
│   └── blackbox-validator/     # HIL / Raspberry Pi GPIO automated test harness
├── docs/                       # Project documentation & visual assets
│   ├── assets/                 # Centralized visual media & breadboard photos
│   │   ├── eeprom-programmer/  # Programmer breadboard & wiring photos
│   │   ├── offline-validator/  # Simulation output & truth table logs
│   │   └── blackbox-validator/ # Pi Zero W bridge & test rig visuals
│   ├── eeprom-programmer/      # EEPROM programmer documentation modules
│   ├── offline-validator/      # Offline validator design documentation
│   └── blackbox-validator/     # Blackbox validator hardware guides
├── LICENSE                     # MIT Open Source License
└── README.md                   # Main project documentation
```

# Project Evolution & Key Phases
## Phase 1: Hardware Design & Core FSM Logic
The initial phase focused on designing the state diagrams, mapping the required logic, and physically verifying the state transitions on hardware.
* **Logic Mapping:** The core state transitions were designed and mapped into 512 hexadecimal address-data pairs.
* **Initial Deployment:** An AT28C64B EEPROM was utilized as the primary logic unit.
* **Hardware Validation:** Breadboard testing confirmed that the fundamental airlock sequencing (idle, pressurizing, doors open, depressurizing) functioned according to the defined truth tables.

### Design Evolution: The Timeout Limitation
During Phase 1, the original architectural goal was to eliminate the need for external hardware timers (like a 555 timer or dedicated RTC chip). The theoretical approach was to simulate hardware timeouts by chaining sequential dummy states within the EEPROM. By driving the EEPROM at a known clock rate, a sequence of redundant states would act as a delay before triggering the next physical airlock action.

However, this approach severely bloated the memory footprint. Because unrolling time into discrete memory addresses grows linearly with the duration of the delay, the AT28C16's strict 512-byte capacity was quickly exhausted. Attempting to fit operational logic, error states, and long timer delays into a 512-line map became impossible.

To circumvent this limitation, the mock-timer approach was ditched. The architecture was refactored to utilize a standard clock signal for state sequencing, offloading the timing responsibility and freeing up the EEPROM to act strictly as the logic matrix.

## Phase 2: Custom EEPROM Reader/Writer (Arduino)
To facilitate the rapid testing, flashing, and verification of the EEPROM logic maps, a custom hardware programmer was built from scratch.
* **Hardware Setup:** The programmer is driven by an Arduino, acting as the primary controller for reading and writing data to the EEPROM.
* **Pin Expansion:** Because addressing the EEPROM requires more I/O pins than a standard Arduino can natively provide, the circuit utilizes two daisy-chained 74HC595N shift registers. This allows the Arduino to efficiently shift out the required address lines using only a few control pins, freeing up the remaining GPIOs to handle data transfer and write-enable signals.
* **Workflow:** This setup allows for completely automated dumping of EEPROM contents and precise flashing of new hexadecimal state maps directly from the Arduino environment.

## Phase 3: Automated C# Verification & Remote Headless Workflow
With the core hardware logic validated and flashing automated, the project is transitioning to a headless verification environment designed for rapid iteration.
* **Hardware Bridge:** The system utilizes a Raspberry Pi Zero W connected to an upgraded AT28C64B EEPROM via a T-Cobbler, establishing a direct GPIO-to-EEPROM bridge operating safely at 3.3V logic levels.
* **Environment Architecture:** Due to the Pi Zero W's ARMv6 architecture constraints, standard modern .NET SDKs could not be deployed. Instead, the Mono JIT compiler (`mono-runtime`) was compiled and configured, providing a highly efficient footprint that bypasses the Zero's RAM and swap limitations.
* **Automated Verification (C#):** The testing suite is written entirely in C#. It allows for programmatic stepping through the FSM sequences, automated verification of output pins against expected state tables, and rapid regression testing of new EEPROM maps without manually swapping chips.
* **Remote Development:** The Pi Zero W is configured entirely as a headless node. All compilation and development occur remotely on a local machine, with execution and monitoring handled seamlessly via SSH.

## Infrastructure Sanity Checks
Before executing exhaustive state-transition sweeps, the test harness provides an **Infrastructure Sanity Check** suite (Option 1). This allows you to manually verify individual signal lines, square-wave clock pulsing, and bus response isolation to ensure your breadboard connections and jumper integrity are optimal prior to full automation runs.

## Roadmap: Standalone Hardware Demo
While the C# test runner and Raspberry Pi serve as an automated black-box regression engine for truth-table validation, the ultimate physical target is a standalone demonstration board.

### Planned Standalone Architecture:
* **Clock Source:** Dedicated 555 timer circuit configured for manual or low-frequency automated square-wave generation.
* **Inputs:** Physical pushbuttons with RC debouncing networks tied directly to address lines.
* **Outputs:** Discrete LED indicator banks driven directly by the EEPROM output bus (`I/O4` - `I/O7`) to visualize state actuator responses in real time without software dependency.

## Acknowledgements & Project Credits
* **Lead Hardware & Software Architect:** Aca Milovanović — Conception, state-machine patterns, breadboard circuit design, C# test harness implementation, firmware development and physical testing.
* **AI Technical Co-Pilot:** Gemini — Architectural brainstorming, state-machine verification patterns, test runner debugging strategies and technical documentation.

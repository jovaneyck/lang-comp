pub enum ParrotType {
    European,
    African,
    NorwegianBlue,
}

pub struct Parrot {
    pub parrot_type: ParrotType,
    pub number_of_coconuts: usize,
    pub voltage: f64,
    pub nailed: bool,
}

impl Parrot {
    pub fn speed(&self) -> f64 {
        match self.parrot_type {
            ParrotType::European => base_speed(),
            ParrotType::African => {
                let african_speed = base_speed() - load_factor() * self.number_of_coconuts as f64;
                if african_speed > 0.0 {
                    african_speed
                } else {
                    0.0
                }
            }
            ParrotType::NorwegianBlue => {
                if self.nailed {
                    0.0
                } else {
                    compute_base_speed_for_voltage(self.voltage)
                }
            }
        }
    }

    pub fn cry(&self) -> &str {
        match self.parrot_type {
            ParrotType::European => "Sqoork!",
            ParrotType::African => "Sqaark!",
            ParrotType::NorwegianBlue => {
                if self.voltage > 0.0 {
                    "Bzzzzzz"
                } else {
                    "..."
                }
            }
        }
    }
}

fn compute_base_speed_for_voltage(voltage: f64) -> f64 {
    let fixed_base_speed = 24.0;
    let base_speed_for_voltage = voltage * base_speed();
    if base_speed_for_voltage < fixed_base_speed {
        base_speed_for_voltage
    } else {
        fixed_base_speed
    }
}

fn load_factor() -> f64 {
    9.0
}

fn base_speed() -> f64 {
    12.0
}

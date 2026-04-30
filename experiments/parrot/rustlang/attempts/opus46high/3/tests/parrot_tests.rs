use parrot::{Parrot, ParrotType};

#[test]
fn european_parrot_speed() {
    let parrot = Parrot {
        parrot_type: ParrotType::European,
        number_of_coconuts: 0,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 12.0);
}

#[test]
fn african_parrot_speed_with_one_coconut() {
    let parrot = Parrot {
        parrot_type: ParrotType::African,
        number_of_coconuts: 1,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 3.0);
}

#[test]
fn african_parrot_speed_with_two_coconuts() {
    let parrot = Parrot {
        parrot_type: ParrotType::African,
        number_of_coconuts: 2,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 0.0);
}

#[test]
fn african_parrot_speed_with_no_coconuts() {
    let parrot = Parrot {
        parrot_type: ParrotType::African,
        number_of_coconuts: 0,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 12.0);
}

#[test]
fn nailed_norwegian_blue_parrot() {
    let parrot = Parrot {
        parrot_type: ParrotType::NorwegianBlue,
        number_of_coconuts: 0,
        voltage: 1.5,
        nailed: true,
    };
    assert_eq!(parrot.speed(), 0.0);
}

#[test]
fn not_nailed_norwegian_blue_parrot() {
    let parrot = Parrot {
        parrot_type: ParrotType::NorwegianBlue,
        number_of_coconuts: 0,
        voltage: 1.5,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 18.0);
}

#[test]
fn not_nailed_norwegian_blue_parrot_with_high_voltage() {
    let parrot = Parrot {
        parrot_type: ParrotType::NorwegianBlue,
        number_of_coconuts: 0,
        voltage: 4.0,
        nailed: false,
    };
    assert_eq!(parrot.speed(), 24.0);
}

#[test]
fn cry_of_european_parrot() {
    let parrot = Parrot {
        parrot_type: ParrotType::European,
        number_of_coconuts: 0,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.cry(), "Sqoork!");
}

#[test]
fn cry_of_african_parrot() {
    let parrot = Parrot {
        parrot_type: ParrotType::African,
        number_of_coconuts: 0,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.cry(), "Sqaark!");
}

#[test]
fn cry_norwegian_blue_parrot_high_voltage() {
    let parrot = Parrot {
        parrot_type: ParrotType::NorwegianBlue,
        number_of_coconuts: 0,
        voltage: 4.0,
        nailed: false,
    };
    assert_eq!(parrot.cry(), "Bzzzzzz");
}

#[test]
fn cry_norwegian_blue_parrot_no_voltage() {
    let parrot = Parrot {
        parrot_type: ParrotType::NorwegianBlue,
        number_of_coconuts: 0,
        voltage: 0.0,
        nailed: false,
    };
    assert_eq!(parrot.cry(), "...");
}

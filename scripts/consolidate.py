"""Consolidate all experiment log.csv files into a single results.csv.

Usage:
    python scripts/consolidate.py

Scans {lang}/attempts/{model}/{n}/log.csv across all languages,
prefixes each row with model, language, and attempt number,
and writes results.csv to the repository root.
"""

import csv
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
OUTPUT = ROOT / "results.csv"


def find_log_files():
    return sorted(ROOT.rglob("attempts/*/*/log.csv"))


def parse_path(log_path):
    """Extract model, language, and attempt number from path like {lang}/attempts/{model}/{n}/log.csv"""
    parts = log_path.relative_to(ROOT).parts
    attempts_idx = parts.index("attempts")
    return {
        "language": parts[attempts_idx - 1],
        "model": parts[attempts_idx + 1],
        "attempt": parts[attempts_idx + 2],
    }


COLUMNS = ["tokens", "start_time", "end_time", "number_of_builds_test_runs_needed", "description"]


def is_header_row(fields):
    return any(f.strip() in COLUMNS for f in fields if f)


def read_log(log_path):
    with open(log_path, newline="", encoding="utf-8") as f:
        reader = csv.reader(f, delimiter=";")
        all_lines = [row for row in reader if any(cell.strip() for cell in row)]

    if not all_lines:
        return []

    data_lines = all_lines[1:] if is_header_row(all_lines[0]) else all_lines

    rows = []
    for line in data_lines:
        values = [v.strip() for v in line]
        row = dict(zip(COLUMNS, values))
        if any(row.values()):
            rows.append(row)
    return rows


def main():
    log_files = find_log_files()
    if not log_files:
        print("No log.csv files found.")
        return

    all_rows = []
    for log_path in log_files:
        meta = parse_path(log_path)
        for row in read_log(log_path):
            all_rows.append({**meta, **row})

    output_columns = ["model", "language", "attempt"] + COLUMNS

    with open(OUTPUT, "w", newline="", encoding="utf-8") as f:
        writer = csv.DictWriter(f, fieldnames=output_columns, delimiter=";", extrasaction="ignore")
        writer.writeheader()
        writer.writerows(all_rows)

    print(f"Wrote {len(all_rows)} rows to {OUTPUT}")


if __name__ == "__main__":
    main()

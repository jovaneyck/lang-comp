"""Interactive dashboard comparing a single model's performance across languages.

Usage:
    python scripts/visualize.py

Change MODEL below to switch which model to analyze.
Opens a browser with an interactive Plotly dashboard.
"""

import pandas as pd
import plotly.express as px
import plotly.graph_objects as go
from plotly.subplots import make_subplots
from pathlib import Path

# ── Configuration ──
MODEL = "opus46high"

ROOT = Path(__file__).resolve().parent.parent
CSV = ROOT / "results.csv"


def load():
    df = pd.read_csv(CSV, sep=";")
    df["start_time"] = pd.to_datetime(df["start_time"])
    df["end_time"] = pd.to_datetime(df["end_time"])
    df["duration_s"] = (df["end_time"] - df["start_time"]).dt.total_seconds()
    df["tokens"] = pd.to_numeric(df["tokens"], errors="coerce")
    df["number_of_builds_test_runs_needed"] = pd.to_numeric(
        df["number_of_builds_test_runs_needed"], errors="coerce"
    )
    df["attempt"] = df["attempt"].astype(str)
    df = df[df["model"] == MODEL]
    return df


def build_dashboard(df):
    fig = make_subplots(
        rows=2, cols=2,
        subplot_titles=(
            "Duration (seconds) by Language",
            "Build/Test Runs Needed by Language",
            "Tokens by Language",
            "Duration vs Build/Test Runs",
        ),
        vertical_spacing=0.12,
        horizontal_spacing=0.1,
    )

    colors = px.colors.qualitative.Set2
    languages = sorted(df["language"].unique())
    color_map = {lang: colors[i % len(colors)] for i, lang in enumerate(languages)}

    for lang in languages:
        sub = df[df["language"] == lang]
        c = color_map[lang]
        hover = sub.apply(
            lambda r: f"Lang: {r['language']}<br>Attempt: {r['attempt']}<br>Duration: {r['duration_s']:.1f}s<br>Builds: {r['number_of_builds_test_runs_needed']}",
            axis=1,
        )

        fig.add_trace(go.Box(y=sub["duration_s"], name=lang, marker_color=c, legendgroup=lang, showlegend=True), row=1, col=1)
        fig.add_trace(go.Box(y=sub["number_of_builds_test_runs_needed"], name=lang, marker_color=c, legendgroup=lang, showlegend=False), row=1, col=2)
        fig.add_trace(go.Box(y=sub["tokens"], name=lang, marker_color=c, legendgroup=lang, showlegend=False), row=2, col=1)
        fig.add_trace(go.Scatter(
            x=sub["number_of_builds_test_runs_needed"],
            y=sub["duration_s"],
            mode="markers",
            marker=dict(color=c, size=10),
            name=lang,
            legendgroup=lang,
            showlegend=False,
            text=hover,
            hoverinfo="text",
        ), row=2, col=2)

    fig.update_yaxes(title_text="Seconds", row=1, col=1)
    fig.update_yaxes(title_text="Runs", row=1, col=2)
    fig.update_yaxes(title_text="Tokens", row=2, col=1)
    fig.update_yaxes(title_text="Duration (s)", row=2, col=2)
    fig.update_xaxes(title_text="Build/Test Runs", row=2, col=2)

    fig.update_layout(
        title_text=f"{MODEL} — Cross-Language Comparison",
        height=800,
        template="plotly_white",
    )
    return fig


def main():
    df = load()
    print(f"Loaded {len(df)} rows for {MODEL}: {df['language'].nunique()} languages")
    print(df.groupby("language")[["duration_s", "number_of_builds_test_runs_needed"]].mean().round(1).to_string())
    print()
    fig = build_dashboard(df)
    html_path = ROOT / "results-dashboard.html"
    fig.write_html(html_path, include_plotlyjs=True)
    print(f"Exported to {html_path}")
    fig.show()


if __name__ == "__main__":
    main()

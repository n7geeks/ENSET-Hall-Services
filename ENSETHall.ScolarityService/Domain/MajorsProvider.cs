using ENSETHall.ScolarityService.Models;

namespace ENSETHall.ScolarityService.Domain;

public static class MajorsProvider
{
	public static string GetEnsetMajors()
	{
		return new string[]
		{
			new MajorDetails(
				"glsid",
				"Génie du Logiciel et des Systèmes Informatiques Distribués",
				"math-info",
				"engineering",
				"init"),
			new MajorDetails(
				"ii-bdcc",
				"Ingénierie Informatique : Big Data et Cloud Computing",
				"math-info",
				"engineering",
				"init"),
			new MajorDetails(
				"sdia",
				"Systèmes Distribués et Intelligence Artificielle",
				"math-info",
				"master-res",
				"init"),
			new MajorDetails(
				"mrmi",
				"Mécanique, Robotique et Matériaux Innovants",
				"genie-meca",
				"master-res",
				"init"),
			new MajorDetails(
				"gmsi",
				"Génie Mécanique des Systèmes Industriels",
				"genie-meca",
				"engineering",
				"init"),
			new MajorDetails(
				"gil",
				"Génie Industriel et Logistique",
				"genie-meca",
				"engineering",
				"init"),
			new MajorDetails(
				"gecsi",
				"Génie Electrique et Contrôle des Systèmes Industriels",
				"genie-elect",
				"engineering",
				"init"),
			new MajorDetails(
				"seer",
				"Génie Electrique option : Systèmes Electriques et Energies Renouvelables",
				"genie-elect",
				"engineering",
				"init"),
			new MajorDetails(
				"eseg",
				"Education et Sciences Economiques et de Gestion",
				"genie-eco",
				"lep",
				"init"),
			new MajorDetails(
				"fc",
				"Finance et Comptabilité",
				"genie-eco",
				"dut",
				"init"),
			new MajorDetails(
				"aoe",
				"Administration et Organisations des Entreprises",
				"staic",
				"dut",
				"init"),
		}.Aggregate((a, b) => a + ", " + b);
	}
}
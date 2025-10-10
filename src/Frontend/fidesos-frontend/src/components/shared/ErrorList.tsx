interface ErrorListProps {
	errors: string[];
}

export function ErrorList({ errors }: ErrorListProps) {
	if (!errors || errors.length === 0) {
		return null;
	}

	if (errors.length === 1) {
		return <p>{errors[0]}</p>;
	}

	return (
		<ul className="list-disc pl-5 text-left">
			{errors.map((error, index) => (
				<li key={index}>{error}</li>
			))}
		</ul>
	);
}

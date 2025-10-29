"use client";

import { TimePicker } from "@/components/forms/time-picker/time-picker";
import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { BaseFormFieldProps, DatePickerConfig } from "@/types/base-form";
import { format } from "date-fns";
import { ptBR } from "date-fns/locale";
import { CalendarIcon } from "lucide-react";
import { FieldPath, FieldValues } from "react-hook-form";

interface FormDateTimePickerProps<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPath<TFieldValues> = FieldPath<TFieldValues>
> extends BaseFormFieldProps<TFieldValues, TName> {
  config?: DatePickerConfig;
}

function FormDateTimePicker<
  TFieldValues extends FieldValues = FieldValues,
  TName extends FieldPath<TFieldValues> = FieldPath<TFieldValues>
>({
  control,
  name,
  label,
  description,
  required,
  config = {},
  disabled,
  className,
}: FormDateTimePickerProps<TFieldValues, TName>) {
  const {
    minDate,
    maxDate,
    disabledDates = [],
    placeholder = "Selecione uma data",
  } = config;

  return (
    <FormField
      control={control}
      name={name}
      render={({ field }) => (
        <FormItem className={cn("flex flex-col items-start", className)}>
          {label && (
            <FormLabel>
              {label}
              {required && <span className="ml-1 text-red-500">*</span>}
            </FormLabel>
          )}
          <Popover>
            <PopoverTrigger asChild>
              <FormControl>
                <Button
                  variant="outline"
                  className={cn(
                    "w-[280px] justify-start text-left font-normal",
                    !field.value && "text-muted-foreground"
                  )}
                  disabled={disabled}
                >
                  <CalendarIcon className="mr-2 h-4 w-4" />
                  {field.value ? (
                    format(field.value, "dd 'de' MMMM 'de' yyyy 'às' HH:mm", {
                      locale: ptBR,
                    })
                  ) : (
                    <span>{placeholder}</span>
                  )}
                </Button>
              </FormControl>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0">
              <Calendar
                mode="single"
                selected={field.value}
                onSelect={field.onChange}
                disabled={(date) => {
                  if (minDate && date < minDate) return true;
                  if (maxDate && date > maxDate) return true;
                  return disabledDates.some(
                    (disabledDate) => date.getTime() === disabledDate.getTime()
                  );
                }}
                initialFocus
                locale={ptBR}
              />
              <div className="p-3 border-t border-border">
                <TimePicker setDate={field.onChange} date={field.value} />
              </div>
            </PopoverContent>
          </Popover>
          {description && <FormDescription>{description}</FormDescription>}
          <FormMessage />
        </FormItem>
      )}
    />
  );
}

export { FormDateTimePicker };
